using System.Diagnostics;
using System.Text.Json;
using Fireworks.Domain.Logging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fireworks.Infrastructure.Behaviors;

// Infrastructure/Behaviors/AuditBehavior.cs
public class AuditBehavior<TRequest, TResponse>(
    ILogger<AuditBehavior<TRequest, TResponse>> logger,
    IAuditLogger auditLogger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var isAuditTarget = typeof(TRequest).Name.EndsWith("Command") || typeof(TRequest).Name.Contains("Create") ||
                            typeof(TRequest).Name.Contains("Update") || typeof(TRequest).Name.Contains("Delete");

        var stopwatch = Stopwatch.StartNew();
        string requestData = JsonSerializer.Serialize(request);

        try
        {
            var response = await next(cancellationToken);
            stopwatch.Stop();

            if (isAuditTarget)
            {
                await auditLogger.LogAsync(
                    typeof(TRequest).Name,
                    requestData,
                    JsonSerializer.Serialize(response),
                    success: true);
            }

            return response;
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            if (isAuditTarget)
            {
                await auditLogger.LogAsync(
                    typeof(TRequest).Name,
                    requestData,
                    ex.Message,
                    success: false);
            }

            throw;
        }
    }
}