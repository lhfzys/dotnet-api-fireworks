namespace Fireworks.Application.common.Constants;

public static class PermissionPolicies
{
    public const string RequireAdmin = "RequireAdmin";

    public static string FromPermission(string permission) => $"Permission:{permission}";
}