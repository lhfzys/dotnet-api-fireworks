using Serilog;

namespace Fireworks.Api.Configurations;

public static class LoggingServiceRegistration
{
    public static IHostBuilder UseLoggingServices(this IHostBuilder host, IConfiguration configuration)
    {
        host.UseSerilog((_, _, lc) =>
        {
            lc.ReadFrom.Configuration(configuration);
        });

        return host;
    }
}