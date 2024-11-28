using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace KeepMoney.Api;

public class SecurityRequirementsOperationFilter : IOperationFilter
{
    private static readonly OpenApiSecurityRequirement _jwtTokenSecurityRequirement = new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
    };

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var methodAttributes = context.MethodInfo.GetCustomAttributes(true);
        var controllerAttributes = context.MethodInfo.DeclaringType!.GetCustomAttributes(true);

        var hasAllowAnonymous = methodAttributes.OfType<AllowAnonymousAttribute>().Any() ||
                                controllerAttributes.OfType<AllowAnonymousAttribute>().Any();

        operation.Security = new List<OpenApiSecurityRequirement>();

        if (!hasAllowAnonymous)
        {
            operation.Security.Add(_jwtTokenSecurityRequirement);
        }
    }
}

