using System.Reflection;
using Elastic.CommonSchema.Serilog;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using SerilogElasticKibana.Api.Middlewares;

namespace SerilogElasticKibana.Api.StartupConfiguration;

public static class SerilogExtension
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        // var configuration = new ConfigurationBuilder()
        //     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //     .AddJsonFile($"appsettings.{environment}.json", optional: true)
        //     .Build();

        if (environment == null) 
            return null;
        var applicationName = Assembly.GetExecutingAssembly().GetName().Name;
        if (applicationName == null)
            return null;
        
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ApplicationName", $"{applicationName} - {environment}")
            .Enrich.WithCorrelationId()
            .Enrich.WithExceptionDetails()
            .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
            .WriteTo.Async(writeTo => 
                writeTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:uri"]))
            {
                TypeName = null,
                AutoRegisterTemplate = true,
                IndexFormat = $"{applicationName.ToLower()}-{DateTime.UtcNow:yyyy-MM}",
                BatchAction = ElasticOpType.Create,
                CustomFormatter = new EcsTextFormatter(),
                // OverwriteTemplate = true,
                // DetectElasticsearchVersion = true,
                // AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                // NumberOfReplicas = 1,
                // NumberOfShards = 2,
                // FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                // EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                //                    EmitEventFailureHandling.WriteToFailureSink |
                //                    EmitEventFailureHandling.RaiseCallback |
                //                    EmitEventFailureHandling.ThrowException,
                //CustomFormatter = new ElasticsearchJsonFormatter()
             }))
            .WriteTo.Async(writeTo => 
                                writeTo.Console(
                                                  outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
                                                  theme: SystemConsoleTheme.Colored
                                ))
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog(Log.Logger, true);

        return builder;
    }

    public static WebApplication UseSerilog(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseSerilogRequestLogging(opts =>
        {
            opts.EnrichDiagnosticContext = LogEnricherExtensions.EnrichFromRequest;
        });

        return app;
    }
}