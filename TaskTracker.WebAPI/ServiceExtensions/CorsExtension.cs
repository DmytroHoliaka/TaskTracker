namespace TaskTracker.WebAPI.ServiceExtensions;

public static class CorsExtension
{
    public static IServiceCollection AddCorsConfiguration(
        this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.WithOrigins("https://localhost:5173");
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                });
        });

        return services;
    }
}
