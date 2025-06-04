using Fireworks.Api.Extensions;
using Fireworks.Api.Interfaces;
using Fireworks.Application.Features.Permissions.CreatePermission;
using Fireworks.Application.Features.Permissions.DeletePermission;
using Fireworks.Application.Features.Permissions.GetAllPermissions;
using Fireworks.Application.Features.Permissions.UpdatePermission;
using MediatR;

namespace Fireworks.Api.Endpoints;

public class PermissionEndpoints : IEndpointRegistrar
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/permission").WithTags("Permission").RequireAuthorization();

        group.MapPost("/", async (IMediator mediator, CreatePermissionRequest request) =>
            (await mediator.Send(request)).ToCustomMinimalApiResult());

        group.MapPut("/", async (IMediator mediator, UpdatePermissionRequest request) =>
            (await mediator.Send(request)).ToCustomMinimalApiResult());

        group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
            (await mediator.Send(new DeletePermissionRequest(id))).ToCustomMinimalApiResult());

        group.MapGet("/", async (IMediator mediator, [AsParameters] GetPermissionsRequest request) =>
        (await mediator.Send(request)).ToCustomMinimalApiResult());
    }
}