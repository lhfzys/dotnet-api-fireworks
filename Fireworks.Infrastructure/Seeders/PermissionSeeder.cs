using Fireworks.Domain.Constants;
using Fireworks.Domain.Entities;
using Fireworks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fireworks.Infrastructure.Seeders;

public static class PermissionSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    { 
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        if (await db.Permissions.AnyAsync()) return;
        
        var system = new Permission
        {
            Name = "系统管理",
            Code = "System",
            Type = PermissionType.Directory,
            Order = 1
        };
        
        await db.Permissions.AddAsync(system);
        await db.SaveChangesAsync();
        
        var children = new List<Permission>
        {
            new()
            {
                Name = "用户管理",
                Code = "System:User",
                Type = PermissionType.Menu,
                ParentId = system.Id,
                Url = "/system/user",
                Order = 1
            },
            new()
            {
                Name = "角色管理",
                Code = "System:Role",
                Type = PermissionType.Menu,
                ParentId = system.Id,
                Url = "/system/role",
                Order = 2
            },
            new()
            {
                Name = "权限管理",
                Code = "System:Permission",
                Type = PermissionType.Menu,
                ParentId = system.Id,
                Url = "/system/permission",
                Order = 3
            }
        };

        await db.Permissions.AddRangeAsync(children);
        await db.SaveChangesAsync();
    }
}