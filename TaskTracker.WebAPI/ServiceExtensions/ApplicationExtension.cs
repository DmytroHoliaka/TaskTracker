using TaskTracker.BLL.Abstractions;
using TaskTracker.DAL.EntityFramework;

namespace TaskTracker.WebAPI.ServiceExtensions;

public static class ApplicationExtension
{
    public static IServiceCollection AddCustomConfiguration(
        this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
