using Fireworks.Api.Extensions;
using Fireworks.Api.Interfaces;
using Fireworks.Application.Features.RolePermissions.UpdateRolePermissions;
using MediatR;

namespace Fireworks.Api.Endpoints;

public class RolePermissionEndpoints : IEndpointRegistrar
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/role-permissions").WithTags("角色权限").RequireAuthorization();

        group.MapPost("/update",
            async (IMediator mediator, UpdateRolePermissionsRequest request) =>
                (await mediator.Send(request)).ToCustomMinimalApiResult());
    }
}