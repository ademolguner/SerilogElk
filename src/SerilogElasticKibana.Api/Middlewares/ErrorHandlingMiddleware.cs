using System.Net;
using System.Text.Json;
using Application.Common.Exceptions;
using Serilog;
using SerilogElasticKibana.Api.Exceptions;

namespace SerilogElasticKibana.Api.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        const string UnexpectedErrorMessage = "Beklenmeyen bir hata ile karşılaşıldı.";
        const string Version = "1.9.0.3";


        switch (exception)
        {
            case ArgumentValidationException validationExp:
                context.Response.HttpContext.Response.StatusCode = validationExp.StatusCode;
                context.Response.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new {Messages = validationExp.MessageProps, Version}));
                break;
            case ClientRequestException clientExp:
                context.Response.HttpContext.Response.StatusCode = clientExp.StatusCode;
                context.Response.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new {clientExp.Message}));
                break;
            case BaseException baseExp:
                context.Response.HttpContext.Response.StatusCode = baseExp.StatusCode;
                context.Response.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new {Messages = new List<string> {baseExp.Message}, Code = baseExp.ErrorCode, Version}));
                break;
            default:
                context.Response.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(new {Messages = new List<string> {UnexpectedErrorMessage}, Version}));
                break;
        }

        var result = JsonSerializer.Serialize(new {error = exception?.Message});

        Log.Error(exception, "Error");
        return context.Response.WriteAsync(result);
    }
}