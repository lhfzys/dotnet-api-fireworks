using Fireworks.Domain.Constants;
using Fireworks.Domain.Entities;
using Fireworks.Domain.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Fireworks.Infrastructure.Persistence;

public static class ApplicationDbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        await context.Database.MigrateAsync();
        
        // 1. 添加权限点
        if (!context.Permissions.Any())
        {
            var permissions = PermissionConstants.All
                .Select(p => new Permission { Name = p })
                .ToList();

            context.Permissions.AddRange(permissions);
            await context.SaveChangesAsync(); 
        }
        
        // 2. 添加超级管理员角色
        const string adminRoleName = "Admin";
        var adminRole = await roleManager.FindByNameAsync(adminRoleName);
        if (adminRole == null)
        {
            adminRole = new ApplicationRole { Name = adminRoleName };
            var result = await roleManager.CreateAsync(adminRole);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to create admin role: " + string.Join(",", result.Errors.Select(e => e.Description)));
            }
        }
        
        // 3. 给 Admin 角色分配所有权限
        var allPermissions = await context.Permissions.ToListAsync();
        var currentPermissions = context.RolePermissions
            .Where(rp => rp.RoleId == adminRole.Id)
            .Select(rp => rp.PermissionId)
            .ToHashSet();
        foreach (var permission in allPermissions)
        {
            if (!currentPermissions.Contains(permission.Id))
            {
                context.RolePermissions.Add(new RolePermission
                {
                    RoleId = adminRole.Id,
                    PermissionId = permission.Id
                });
            }
        }
        await context.SaveChangesAsync();
        
        // 4. 添加超级管理员账户
        const string adminEmail = "admin@system.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(adminUser, "123456");
            if (!createResult.Succeeded)
            {
                throw new Exception("Failed to create admin user: " + string.Join(",", createResult.Errors.Select(e => e.Description)));
            }

            // ⚠️ 这里角色必须已经成功插入，才能加成功
            await userManager.AddToRoleAsync(adminUser, adminRoleName);
        }
    }
}