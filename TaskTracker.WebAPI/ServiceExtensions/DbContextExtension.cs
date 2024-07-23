using Microsoft.EntityFrameworkCore;
using TaskTracker.DAL.EntityFramework;

namespace TaskTracker.WebAPI.ServiceExtensions;

public static class DbContextExtension
{
    public static IServiceCollection AddDbContextConfiguration(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<TaskTrackerContext>(
            options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("TaskTrackerContext") ??
                    throw new InvalidOperationException("Connection string 'TaskTrackerContext' not found.")));

        return services;
    }
}
