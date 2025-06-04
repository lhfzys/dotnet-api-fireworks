using Ardalis.Result;
using Fireworks.Application.Features.UserRoles.AssignRole;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.UserRoles.DeleteRoles;

public class DeleteRolesHandler(UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager) : IRequestHandler<DeleteRoleRequest, Result>
{
    public async Task<Result> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return Result.Invalid(new ValidationError(nameof(request.UserId), "用户不存在"));
        var existingRoles = await roleManager.Roles
            .Where(r => r.Name != null && request.Roles.Contains(r.Name))
            .Select(r => r.Name)
            .ToListAsync(cancellationToken);
        var invalidRoles = request.Roles.Except(existingRoles).ToList();
        if (invalidRoles.Count != 0)
            return Result.Invalid(new ValidationError(
                nameof(request.Roles),
                $"以下角色不存在: {string.Join(", ", invalidRoles)}"));
        var result = await userManager.RemoveFromRolesAsync(user, request.Roles);
        return result.Succeeded
            ? Result.Success()
            : Result.Invalid(new ValidationError(nameof(request.Roles), "删除角色失败"));
    }
}