namespace Fireworks.Domain.Constants;

public static class PermissionConstants
{
    public const string UserRead = "User.Read";
    public const string UserCreate = "User.Create";
    public const string UserUpdate = "User.Update";
    public const string UserDelete = "User.Delete";

    public const string RoleRead = "Role.Read";
    public const string RoleCreate = "Role.Create";
    public const string RoleUpdate = "Role.Update";
    public const string RoleDelete = "Role.Delete";

    public static readonly string[] All =
    [
        UserRead, UserCreate, UserUpdate, UserDelete,
        RoleRead, RoleCreate, RoleUpdate, RoleDelete
    ];
}