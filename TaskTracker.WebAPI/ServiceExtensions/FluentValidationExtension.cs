using FluentValidation;
using MediatR;
using System.Globalization;
using TaskTracker.BLL.Behaviours;

namespace TaskTracker.WebAPI.ServiceExtensions;

public static class FluentValidationExtension
{
    public static IServiceCollection AddFluentValidationConfiguration(
        this IServiceCollection services)
    {
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationPipelineBehavior<,>));

        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US");

        services.AddValidatorsFromAssembly(
            TaskTracker.BLL.AssemblyReference.Assembly,
            includeInternalTypes: true);

        return services;
    }
}
