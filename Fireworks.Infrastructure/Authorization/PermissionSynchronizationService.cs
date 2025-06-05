using Fireworks.Application.common.Constants;
using Fireworks.Domain.Constants;
using Fireworks.Domain.Entities;
using Fireworks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Infrastructure.Authorization;

public class PermissionSynchronizationService(ApplicationDbContext dbContext)
{
    public async Task SyncPermissionsAsync()
    {
        var existingPermissions = await dbContext.Permissions
            .ToDictionaryAsync(p => p.Code);

        foreach (var code in PermissionConstants.All)
        {
            await EnsurePermissionHierarchyAsync(code, existingPermissions);
        }

        await dbContext.SaveChangesAsync();
    }

    private Task EnsurePermissionHierarchyAsync(string fullCode, Dictionary<string, Permission> cache)
    {
        var segments = fullCode.Split(':');
        var currentPath = "";
        Guid? parentId = null;

        for (var i = 0; i < segments.Length; i++)
        {
            currentPath = i == 0 ? segments[i] : $"{currentPath}:{segments[i]}";

            if (cache.TryGetValue(currentPath, out var value))
            {
                parentId = value.Id;
                continue;
            }

            var type = GetPermissionTypeByLevel(i, segments.Length);
            var url = GenerateUrl(currentPath, type);
            var order = GetOrderIndex(segments[i]);

            var permission = new Permission
            {
                Id = Guid.NewGuid(),
                Code = currentPath,
                Name = GenerateNameFromCode(segments[i]),
                Type = GetPermissionTypeByLevel(i, segments.Length),
                ParentId = parentId,
                Order = order,
                IsEnabled = true,
                Url = url
            };

            dbContext.Permissions.Add(permission);
            cache[currentPath] = permission;
            parentId = permission.Id;
        }

        return Task.CompletedTask;
    }

    private static string? GenerateUrl(string code, PermissionType type)
    {
        if (type != PermissionType.Menu) return null;
        var path = code
            .ToLowerInvariant()
            .Replace(":", "/");

        return "/" + path;
    }

    private static int GetOrderIndex(string segment)
    {
        return segment switch
        {
            "System" => 1,
            "User" => 1,
            "Role" => 2,
            "Permission" => 3,
            "AuditLog" => 4,

            "Read" => 1,
            "Create" => 2,
            "Update" => 3,
            "Delete" => 4,
            _ => 99
        };
    }


    private static string GenerateNameFromCode(string codeSegment)
    {
        return codeSegment switch
        {
            "System" => "系统管理",
            "User" => "用户管理",
            "Role" => "角色管理",
            "Permission" => "权限管理",
            "AuditLog" => "审计日志",
            "Create" => "新增",
            "Read" => "查看",
            "Update" => "编辑",
            "Delete" => "删除",
            _ => codeSegment
        };
    }

    private static PermissionType GetPermissionTypeByLevel(int level, int totalSegments)
    {
        return level switch
        {
            0 => PermissionType.Directory,
            1 when totalSegments == 2 => PermissionType.Menu,
            _ => PermissionType.Button
        };
    }
}