using Ardalis.Result;
using Fireworks.Application.common.Interfaces;
using Fireworks.Domain.Constants;
using Fireworks.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Application.Features.Menus;

public class GetCurrentUserMenuHandler(
    ICurrentUserService currentUser,
    IApplicationDbContext db) : IRequestHandler<GetCurrentUserMenuRequest, Result<List<MenuResponse>>>
{
    public async Task<Result<List<MenuResponse>>> Handle(GetCurrentUserMenuRequest request,
        CancellationToken cancellationToken)
    {
        var userId = currentUser.Id;
        if (userId == Guid.Empty) return Result.Invalid(new ValidationError(nameof(userId), "未找到用户信息"));

        var roleNames = currentUser.Roles;
        var roleIds = await db.Roles
            .Where(r => r.Name != null && roleNames.Contains(r.Name))
            .Select(r => r.Id)
            .ToListAsync(cancellationToken);
        if (roleIds.Count == 0)
            return Result<List<MenuResponse>>.Success([]);
        var permissionIds = await db.RolePermissions
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.PermissionId)
            .Distinct()
            .ToListAsync(cancellationToken);
        var allPermissions = await db.Permissions
            .Where(p => permissionIds.Contains(p.Id) &&
                        (p.Type == PermissionType.Menu || p.Type == PermissionType.Directory) &&
                        p.IsEnabled)
            .ToListAsync(cancellationToken);

        var topLevelMenus = allPermissions
            .Where(p => p.ParentId == null)
            .OrderBy(p => p.Order)
            .Select(p => MapToMenuResponse(p, allPermissions))
            .ToList();
        Console.WriteLine(topLevelMenus);
        return Result<List<MenuResponse>>.Success(topLevelMenus);
    }

    private static MenuResponse MapToMenuResponse(
        Permission permission,
        List<Permission> allPermissions)
    {
        var children = allPermissions
            .Where(p => p.ParentId == permission.Id)
            .OrderBy(p => p.Order)
            .Select(p => MapToMenuResponse(p, allPermissions))
            .ToList();

        return new MenuResponse(
            permission.Id,
            permission.Name,
            permission.Icon,
            permission.Url,
            permission.Order,
            children);
    }
}