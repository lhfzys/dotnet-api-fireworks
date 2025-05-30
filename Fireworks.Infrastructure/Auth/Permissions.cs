namespace Fireworks.Infrastructure.Auth;

public static class Permissions
{
    public static readonly string[] All =
    [
        "User.Create",
        "User.Read",
        "User.Update",
        "User.Delete",
        "Role.Create",
        "Role.Read",
        "Role.Update",
        "Role.Delete",
        "Permission.Create",
        "Permission.Assign"
    ];
}