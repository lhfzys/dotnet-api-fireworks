namespace Fireworks.Infrastructure.Cache;

public static class CacheOptions
{
    public static TimeSpan Short => TimeSpan.FromMinutes(5);
    public static TimeSpan Medium => TimeSpan.FromMinutes(30);
    public static TimeSpan Long => TimeSpan.FromHours(2);
}
