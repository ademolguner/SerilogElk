using System.Net;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SerilogElasticKibana.Api.Exceptions;

namespace API.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ExceptionFilterAttribute : Attribute, IExceptionFilter
{
    private const string UnexpectedErrorMessage = "Beklenmeyen bir hata ile karşılaşıldı.";
    private const string Version = "1.0.0";

    public virtual void OnException(ExceptionContext context)
    {
        if (context.Exception is ArgumentValidationException validationExp)
        {
            context.HttpContext.Response.StatusCode = validationExp.StatusCode;
            context.Result = new ObjectResult(new {Messages = validationExp.MessageProps, Version});
            return;
        }

        if (context.Exception is ClientRequestException clientExp)
        {
            context.HttpContext.Response.StatusCode = clientExp.StatusCode;
            context.Result = new ObjectResult(new {clientExp.Messages, Code = clientExp.ErrorCode, Version});
            return;
        }

        if (context.Exception is BaseException baseExp)
        {
            context.HttpContext.Response.StatusCode = baseExp.StatusCode;
            context.Result = new ObjectResult(new
                {Messages = new List<string> {baseExp.Message}, Code = baseExp.ErrorCode, Version});
            return;
        }

        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new {Messages = new List<string> {UnexpectedErrorMessage}, Version});
    }
}