namespace Fireworks.Application.common.Constants;

public static class PermissionConstants
{
    public const string UserRead = "System:User:Read";
    public const string UserCreate = "System:User:Create";
    public const string UserUpdate = "System:User:Update";
    public const string UserDelete = "System:User:Delete";

    public const string RoleRead = "System:Role:Read";
    public const string RoleCreate = "System:Role:Create";
    public const string RoleUpdate = "System:Role:Update";
    public const string RoleDelete = "System:Role:Delete";

    public const string PermissionRead = "System:Permission:Read";
    public const string PermissionCreate = "System:Permission:Create";
    public const string PermissionUpdate = "System:Permission:Update";
    public const string PermissionDelete = "System:Permission:Delete";

    public const string AuditLogRead = "System:AuditLog:Read";

    public static readonly string[] All =
    [
        UserRead, UserCreate, UserUpdate, UserDelete,
        RoleRead, RoleCreate, RoleUpdate, RoleDelete,
        PermissionRead, PermissionCreate, PermissionUpdate, PermissionDelete,
        AuditLogRead
    ];
}