using TaskTracker.BLL.Profiles;

namespace TaskTracker.WebAPI.ServiceExtensions
{
    public static class AutoMapperExtension
    {
        public static IServiceCollection AddAutoMapperConfiguration(
            this IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile<TodoItemProfile>();
            });

            return services;
        }
    }
}
