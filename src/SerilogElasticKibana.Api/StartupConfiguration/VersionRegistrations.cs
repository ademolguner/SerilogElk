using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace SerilogElasticKibana.Api.StartupConfiguration;

public static class VersionRegistrations
{
    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
    {
        services
            .AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            })
            .AddMvcCore()
            .AddApiExplorer()
            .AddNewtonsoftJson();
        return services;
    }
}