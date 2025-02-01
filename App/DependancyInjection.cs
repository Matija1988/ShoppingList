using App.Abstractions.Behaviours;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace App;

public static class DependancyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly);

            config.AddOpenBehavior(typeof(RequestLoggingPipelineBahavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependancyInjection).Assembly,
            includeInternalTypes: true);

        return services;
    }
}
