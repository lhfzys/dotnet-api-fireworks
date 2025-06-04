using Ardalis.Result;
using Fireworks.Domain.Identity.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.UserRoles.AssignRole;

public class AssignRoleHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    : IRequestHandler<AssignRoleRequest, Result>
{
    public async Task<Result> Handle(AssignRoleRequest request, CancellationToken cancellationToken)
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
        var result = await userManager.AddToRolesAsync(user, request.Roles);
        return result.Succeeded
            ? Result.Success()
            : Result.Invalid(new ValidationError(nameof(request.Roles), "添加角色失败"));
    }
}