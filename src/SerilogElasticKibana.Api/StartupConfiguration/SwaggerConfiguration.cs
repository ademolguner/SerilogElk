using System.Reflection;
using Microsoft.OpenApi.Models;
using SerilogElasticKibana.Api.Filters;

namespace SerilogElasticKibana.Api.StartupConfiguration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("SwaggerSettings");
        services.AddSwaggerGen(options =>
        {
            var apiName = section.GetValue<string>("Name");
            var apiDesc = section.GetValue<string>("Description");

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = apiName,
                Version = "v1",
                Description = apiDesc
            });

            options.OperationFilter<SwaggerJsonIgnoreFilter>();

            var folder = Environment.CurrentDirectory.Replace(Assembly.GetExecutingAssembly().GetName().Name, "");
            if (!string.IsNullOrEmpty(folder))
                foreach (var name in Directory.GetFiles(folder, "*.xml", SearchOption.AllDirectories))
                    options.IncludeXmlComments(name);

            //options.ExampleFilters();
        });
        //services.AddSwaggerExamplesFromAssemblies(typeof(PreCheckoutCommand).GetTypeInfo().Assembly);
        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var section = configuration.GetSection("SwaggerSettings");
        app.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{section.GetValue<string>("BasePath")}{"v1"}/swagger.json",
                    section.GetValue<string>("Name") + " v1");
            });

        return app;
    }
}