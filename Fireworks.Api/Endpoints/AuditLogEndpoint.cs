using Fireworks.Api.Extensions;
using Fireworks.Api.Interfaces;
using Fireworks.Application.common.Constants;
using Fireworks.Application.Features.AuditLogs.GetAuditLogs;
using MediatR;

namespace Fireworks.Api.Endpoints;

public class AuditLogEndpoint : IEndpointRegistrar
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/auditlog").WithTags("AuditLog")
            .RequireAuthorization(PermissionPolicies.RequireAdmin);
        group.MapGet("/", async (IMediator mediator, [AsParameters] GetAuditLogsRequest request) =>
        (await mediator.Send(request)).ToCustomMinimalApiResult());
    }
}