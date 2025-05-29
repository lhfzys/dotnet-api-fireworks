using Ardalis.Result;
using Fireworks.Application.Features.UserRoles.AssignRole;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fireworks.Application.Features.UserRoles.DeleteRoles;

public class DeleteRolesHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<AssignRoleRequest, Result>
{
    public async Task<Result> Handle(AssignRoleRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return Result.Invalid(new ValidationError(nameof(request.UserId), "用户不存在"));
        var result = await userManager.RemoveFromRolesAsync(user, request.Roles);
        return result.Succeeded
            ? Result.Success()
            : Result.Invalid(new ValidationError(nameof(request.Roles), "删除角色失败"));
    }
}