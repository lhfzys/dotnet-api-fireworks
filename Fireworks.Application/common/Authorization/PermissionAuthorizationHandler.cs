using System.Security.Claims;
using Fireworks.Application.common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.common.Authorization;

public class PermissionAuthorizationHandler(
    IApplicationDbContext dbContext) : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId)) return;

        var roleNames = context.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
        if (!roleNames.Any()) return;

        var roleIds = await dbContext.Roles
            .Where(r => roleNames.Contains(r.Name!))
            .Select(r => r.Id)
            .ToListAsync();

        var hasPermission = await dbContext.RolePermissions
            .Include(rp => rp.Permission)
            .AnyAsync(rp => roleIds.Contains(rp.RoleId) && rp.Permission.Code == requirement.PermissionCode);

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
    }
}