using System;

namespace TaskTracker.WebAPI.ServiceExtensions;

public static class MediatRExtension
{
    public static IServiceCollection AddMediatRConfiguration(
        this IServiceCollection services)
    {
        services.AddMediatR(
            config => config.RegisterServicesFromAssemblies(
                TaskTracker.BLL.AssemblyReference.Assembly));

        return services;
    }
}
