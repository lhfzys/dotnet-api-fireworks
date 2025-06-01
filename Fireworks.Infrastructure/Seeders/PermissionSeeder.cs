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
        
        var permissions = new List<Permission>
        {
            new()
            {
                Name = "系统管理",
                Code = "System",
                Type = PermissionType.Directory,
                Order = 1
            },
            new()
            {
                Name = "用户管理",
                Code = "System:User",
                Type = PermissionType.Menu,
                ParentId = null,
                Url = "/system/user",
                Order = 1
            },
            new()
            {
                Name = "角色管理",
                Code = "System:Role",
                Type = PermissionType.Menu,
                ParentId = null,
                Url = "/system/role",
                Order = 1
            },
            new()
            {
                Name = "权限管理",
                Code = "System:Permission",
                Type = PermissionType.Menu,
                ParentId = null,
                Url = "/system/permission",
                Order = 1
            }
            
        };

        var systemMenu = permissions[0];
        var userMenu = permissions[1];
        var roleMenu = permissions[2];
        var permissionMenu = permissions[3];
        userMenu.Parent = systemMenu;
        roleMenu.Parent = systemMenu;
        permissionMenu.Parent = systemMenu;
        db.Permissions.AddRange(permissions);
        await db.SaveChangesAsync();
    }
}