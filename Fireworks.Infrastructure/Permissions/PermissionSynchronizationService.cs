using System.Reflection;
using Fireworks.Domain.Constants;
using Fireworks.Domain.Entities;
using Fireworks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fireworks.Infrastructure.Permissions;

public class PermissionSynchronizationService(ApplicationDbContext context)
{
    public async Task SyncAsync()
    {
        var permissionAttrs = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(t => t.IsClass && t.GetCustomAttributes<PermissionAttribute>().Any())
            .SelectMany(t => t.GetCustomAttributes<PermissionAttribute>())
            .ToList();

        var dbPermissions = await context.Permissions.ToListAsync();
        var dbPermissionDict = dbPermissions.ToDictionary(p => p.Code);

        foreach (var attr in permissionAttrs)
        {
            if (dbPermissionDict.TryGetValue(attr.Code, out var existing))
            {
                existing.Name = attr.Name;
                existing.Type = attr.Type;
                existing.Parent = attr.ParentCode != null && dbPermissionDict.TryGetValue(attr.ParentCode, out var parent)
                    ? parent
                    : null;
                existing.Order = attr.Order;
            }
            else
            {
                var permission = new Permission
                {
                    Code = attr.Code,
                    Name = attr.Name,
                    Type = attr.Type,
                    Order = attr.Order,
                    Parent = attr.ParentCode != null && dbPermissionDict.TryGetValue(attr.ParentCode, out var parent)
                        ? parent
                        : null
                };
                context.Permissions.Add(permission);
            }
        }

        await context.SaveChangesAsync();
    }
}
