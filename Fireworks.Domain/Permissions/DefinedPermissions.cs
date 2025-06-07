using Fireworks.Infrastructure.Permissions;
using Fireworks.Shared.Permissions;

namespace Fireworks.Domain.Permissions;

[Permission("系统管理", "System", PermissionType.Directory, 1, null)]
[Permission("用户管理", "System:User", PermissionType.Menu, 1, "System")]
[Permission("创建用户", "System:User.Create", PermissionType.Button, 1, "System:User")]
[Permission("删除用户", "System:User.Delete", PermissionType.Button, 2, "System:User")]
[Permission("修改用户", "System:User.Update", PermissionType.Button, 3, "System:User")]
[Permission("查看用户", "System:User.Read", PermissionType.Button, 4, "System:User")]
[Permission("角色管理", "System:Role", PermissionType.Menu, 2, "System")]
[Permission("创建角色", "System:Role.Create", PermissionType.Button, 1, "System:Role")]
[Permission("删除角色", "System:Role.Delete", PermissionType.Button, 2, "System:Role")]
[Permission("修改角色", "System:Role.Update", PermissionType.Button, 3, "System:Role")]
[Permission("查看角色", "System:Role.Read", PermissionType.Button, 4, "System:Role")]
[Permission("权限管理", "System:Permission", PermissionType.Menu, 3, "System")]
[Permission("创建权限", "System:Permission.Create", PermissionType.Button, 1, "System:Permission")]
[Permission("删除权限", "System:Permission.Delete", PermissionType.Button, 2, "System:Permission")]
[Permission("修改权限", "System:Permission.Update", PermissionType.Button, 3, "System:Permission")]
[Permission("查看权限", "System:Permission.Read", PermissionType.Button, 4, "System:Permission")]
[Permission("日志管理", "System:AuditLog", PermissionType.Menu, 4, "System")]
[Permission("查看日志", "System:AuditLog.Read", PermissionType.Button, 4, "System:AuditLog")]
public class DefinedPermissions
{
}