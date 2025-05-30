using Fireworks.Api.Extensions;
using Fireworks.Application.Features.RolePermissions.UpdateRolePermissions;
using MediatR;

namespace Fireworks.Api.Endpoints;

public static class RolePermissionEndpoints
{
    public static void MapRolePermissionEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/role-permissions").WithTags("角色权限");

        group.MapPost("/update",
            async (IMediator mediator, UpdateRolePermissionsRequest request) =>
                (await mediator.Send(request)).ToCustomMinimalApiResult());
    }
}