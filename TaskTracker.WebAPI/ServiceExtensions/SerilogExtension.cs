using Serilog;

namespace TaskTracker.WebAPI.ServiceExtensions;

public static class SerilogExtension
{
    public static IHostBuilder AddSerilogConfiguration(
        this IHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        return host;
    }
}
