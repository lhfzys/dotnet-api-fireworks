using Fireworks.Api.Extensions;
using Fireworks.Application.Features.Roles;
using Fireworks.Application.Features.Roles.CreateRole;
using Fireworks.Application.Features.Roles.DeleteRole;
using Fireworks.Application.Features.Roles.GetAllRoles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fireworks.Api.Endpoints;

public static class RoleEndPoints
{
    public static void MapRoleEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/role").WithTags("Role").RequireAuthorization();

        // 获取所有角色
        group.MapGet("/",
            async (IMediator mediator) =>
                (await mediator.Send(new GetAllRolesRequest())).ToCustomMinimalApiResult());

        // 创建角色
        group.MapPost("/",
            async (IMediator mediator, CreateRoleRequest request) =>
            (await mediator.Send(request)).ToCustomMinimalApiResult());
        
        group.MapDelete("/{roleName}",
            async (string roleName, IMediator mediator) =>
                (await mediator.Send(new DeleteRoleRequest(RoleName: roleName))).ToCustomMinimalApiResult());
    }
}