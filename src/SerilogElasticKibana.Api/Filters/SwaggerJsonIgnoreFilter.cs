using System.Reflection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SerilogElasticKibana.Api.Filters;

public class SwaggerJsonIgnoreFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var ignoredProperties = context.MethodInfo.GetParameters()
            .SelectMany(p => p.ParameterType.GetProperties()
                .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() != null))
            .ToList();

        if (!ignoredProperties.Any()) return;

        foreach (var property in ignoredProperties)
            operation.Parameters = operation.Parameters
                .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture))
                .ToList();
    }
}