using Ardalis.Result;
using Fireworks.Application.common;
using Fireworks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.RolePermissions.UpdateRolePermissions;

public class UpdateRolePermissionsHandler(IApplicationDbContext dbContext)
    : IRequestHandler<UpdateRolePermissionsRequest, Result>
{
    public async Task<Result> Handle(UpdateRolePermissionsRequest request, CancellationToken cancellationToken)
    {
        var role = await dbContext.Roles.Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == request.RoleId, cancellationToken);

        if (role == null)
            return Result.NotFound("角色不存在");

        var existingPermissions = role.RolePermissions.Select(rp => rp.PermissionId).ToHashSet();

        var toRemove = role.RolePermissions
            .Where(rp => !request.PermissionIds.Contains(rp.PermissionId))
            .ToList();

        dbContext.RolePermissions.RemoveRange(toRemove);
        var toAdd = request.PermissionIds
            .Where(pid => !existingPermissions.Contains(pid))
            .Select(pid => new RolePermission
            {
                RoleId = role.Id,
                PermissionId = pid
            });
        await dbContext.RolePermissions.AddRangeAsync(toAdd, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}