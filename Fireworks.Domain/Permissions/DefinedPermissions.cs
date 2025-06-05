using Fireworks.Infrastructure.Permissions;
using Fireworks.Shared.Permissions;

namespace Fireworks.Domain.Permissions;

[Permission("System", "系统管理", PermissionType.Directory, 1, null)]
[Permission("System:User", "用户管理", PermissionType.Menu, 1, "System")]
[Permission("System:User.Create", "创建用户", PermissionType.Button, 1, "System:User")]
[Permission("System:User.Delete", "删除用户", PermissionType.Button, 2, "System:User")]
[Permission("System:User.Update", "修改用户", PermissionType.Button, 3, "System:User")]
[Permission("System:User.Read", "查看用户", PermissionType.Button, 4, "System:User")]
[Permission("System:Role", "角色管理", PermissionType.Menu, 2, "System")]
[Permission("System:Role.Create", "创建角色", PermissionType.Button, 1, "System:Role")]
[Permission("System:Role.Delete", "删除角色", PermissionType.Button, 2, "System:Role")]
[Permission("System:Role.Update", "修改角色", PermissionType.Button, 3, "System:Role")]
[Permission("System:Role.Read", "查看角色", PermissionType.Button, 4, "System:Role")]
[Permission("System:Permission", "权限管理", PermissionType.Menu, 3, "System")]
[Permission("System:Permission.Create", "创建权限", PermissionType.Button, 1, "System:Permission")]
[Permission("System:Permission.Delete", "删除权限", PermissionType.Button, 2, "System:Permission")]
[Permission("System:Permission.Update", "修改权限", PermissionType.Button, 3, "System:Permission")]
[Permission("System:Permission.Read", "查看权限", PermissionType.Button, 4, "System:Permission")]
[Permission("System:AuditLog", "日志管理", PermissionType.Menu, 4, "System")]
[Permission("System:AuditLog.Read", "查看日志", PermissionType.Button, 4, "System:AuditLog")]
public class DefinedPermissions
{
}