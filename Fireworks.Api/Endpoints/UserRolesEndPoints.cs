using System.Runtime.InteropServices;
using Fireworks.Api.Extensions;
using Fireworks.Api.Interfaces;
using Fireworks.Application.common.Constants;
using Fireworks.Application.Features.UserRoles.AssignRole;
using Fireworks.Application.Features.UserRoles.GetUserRoles;
using MediatR;

namespace Fireworks.Api.Endpoints;

public class UserRolesEndPoints : IEndpointRegistrar
{
    public void MapEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/user-roles").WithTags("UserRoles")
            .RequireAuthorization(PermissionPolicies.RequireAdmin);

        // 获取用户所有角色
        group.MapGet("/{userId:guid}", async (Guid userId, IMediator mediator)
            => (await mediator.Send(new GetUserRolesRequest(UserId: userId))).ToCustomMinimalApiResult());

        // 给用户添加角色（追加）
        group.MapPost("/add", async (IMediator mediator, AssignRoleRequest request)
            => (await mediator.Send(request)).ToCustomMinimalApiResult());

        // 替换用户全部角色(全量更新)
        group.MapPost("/replace", async (IMediator mediator, ReplaceRoleRequest request)
            => (await mediator.Send(request)).ToCustomMinimalApiResult());

        // 删除用户角色
        group.MapPost("/remove", async (IMediator mediator, DeleteRoleRequest request) =>
            (await mediator.Send(request)).ToCustomMinimalApiResult());
    }
}