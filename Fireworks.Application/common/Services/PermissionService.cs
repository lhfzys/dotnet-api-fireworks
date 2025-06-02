using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.common.Services;

public class PermissionService(IApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
    : IPermissionService
{


    public async Task<List<string>> GetPermissionsForUserAsync(ApplicationUser user, CancellationToken cancellationToken = default)
    {
        var roles = await userManager.GetRolesAsync(user);
        var permissions = await dbContext.RolePermissions
            .Where(rp => rp.Role.Name != null && roles.Contains(rp.Role.Name))
            .Select(rp => rp.Permission.Name)
            .Distinct()
            .ToListAsync(cancellationToken);

        return permissions;
    }
}