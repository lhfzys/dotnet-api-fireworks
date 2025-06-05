using System.Diagnostics;
using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Constants;
using Fireworks.Domain.Entities;

namespace Fireworks.Api.Middleware;

public class AuditLogMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IAuditLogService auditService, ICurrentUserService currentUser)
    {
        var sw = Stopwatch.StartNew();

        var request = context.Request;
        request.EnableBuffering();

        var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        request.Body.Position = 0;

        var originalBodyStream = context.Response.Body;
        await using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next(context);

        sw.Stop();

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();
        context.Response.Body.Seek(0, SeekOrigin.Begin);

        var log = new AuditLog
        {
            UserId = currentUser.Id,
            UserName = currentUser.UserName,
            HttpMethod = request.Method,
            Url = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}",
            IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            UserAgent = context.Request.Headers["User-Agent"],
            RequestData = requestBody,
            ResponseData = responseText,
            StatusCode = context.Response.StatusCode,
            ExecutionDurationMs = sw.ElapsedMilliseconds
        };

        await auditService.LogAsync(log);

        await responseBody.CopyToAsync(originalBodyStream);
    }
}
