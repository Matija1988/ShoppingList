namespace Web.Api.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection CorsSetup(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("Authorization")
                );
        });

        return services;
    }

    public static IApplicationBuilder ApplyCors(this WebApplication app)
    { 
        app.UseCors("CorsPolicy");
        return app;
    }
}
