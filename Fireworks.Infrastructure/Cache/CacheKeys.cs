namespace Fireworks.Infrastructure.Cache;

public static class CacheKeys
{
    public static string User(Guid userId) => $"user:{userId}";
    public static string UserMenu(Guid userId) => $"user:{userId}:menu";
    public static string Role(Guid roleId) => $"role:{roleId}";
    public static string AllPermissions => "permissions:all";
    public static string SystemSettings => "system:settings";
}
