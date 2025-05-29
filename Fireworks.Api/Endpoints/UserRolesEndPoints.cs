using System.Runtime.InteropServices;
using Fireworks.Api.Extensions;
using Fireworks.Application.Features.UserRoles.AssignRole;
using Fireworks.Application.Features.UserRoles.GetUserRoles;
using MediatR;

namespace Fireworks.Api.Endpoints;

public static class UserRolesEndPoints
{
    public static void MapUserRolesEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/user-roles").WithTags("UserRoles").RequireAuthorization();

        // 获取用户所有角色
        group.MapGet("/{userId:guid}", async (Guid userId, IMediator mediator)
            => (await mediator.Send(new GetUserRolesRequest(UserId: userId))).ToCustomMinimalApiResult());

        // 给用户添加角色（追加）
        group.MapPost("/add", async (IMediator mediator, AssignRoleRequest request)
            => (await mediator.Send(request)).ToCustomMinimalApiResult());
        
        // 替换用户全部角色(全量更新)
        group.MapPost("/replace", async (IMediator mediator, AssignRoleRequest request)
            => (await mediator.Send(request)).ToCustomMinimalApiResult());
        
        // 删除用户角色
        group.MapPost("/remove", async (IMediator mediator, AssignRoleRequest request) =>
            (await mediator.Send(request)).ToCustomMinimalApiResult());
    }
}