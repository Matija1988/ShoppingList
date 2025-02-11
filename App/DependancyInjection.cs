using App.Abstractions.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace App;
/// <summary>
/// DI modul za dodavanje i konfiguraciju ovisnosti u App layeru.
/// MediatR - implementacija Mediator patterna za Request-Response arhitekturu
/// </summary>
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
