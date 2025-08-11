namespace TaskTracker.WebAPI.ServiceExtensions;

public static class CommonServiceExtension
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
