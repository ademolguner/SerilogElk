using Microsoft.AspNetCore.Http.Features;
using Serilog;

namespace SerilogElasticKibana.Api.StartupConfiguration;

public static class LogEnricherExtensions
{
    public static void EnrichFromRequest(IDiagnosticContext diagnosticContext, HttpContext httpContext)
    {
        diagnosticContext.Set("UserName", httpContext?.User?.Identity?.Name);
        diagnosticContext.Set("ClientIP", httpContext?.Connection?.RemoteIpAddress?.ToString());
        diagnosticContext.Set("UserAgent", httpContext?.Request?.Headers?["User-Agent"].FirstOrDefault());
        diagnosticContext.Set("Resource", httpContext?.GetMetricsCurrentResourceName());
    }

    private static string? GetMetricsCurrentResourceName(this HttpContext httpContext)
    {
        if (httpContext == null)
            throw new ArgumentNullException(nameof(httpContext));

        var endpoint = httpContext?.Features?.Get<IEndpointFeature>()?.Endpoint;

        return endpoint?.Metadata?.GetMetadata<EndpointNameMetadata>()?.EndpointName;
    }
}