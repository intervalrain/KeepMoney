using Microsoft.OpenApi.Models;

namespace KeepMoney.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.ConfigureSwaggerAuthorizationSchemes();

        return services;
    }

    public static IServiceCollection ConfigureSwaggerAuthorizationSchemes(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Keep Money API",
                Version = "v1",
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        return services;
    }
}

