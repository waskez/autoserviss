using AutoServiss.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace AutoServiss.Server.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";

            if (context.Exception is ValidationException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new
                {
                    Message = "Validācijas kļūda",
                    Errors = ((ValidationException)context.Exception).Failures.Values
                });
            }
            else if (context.Exception is NotFoundException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;//.NotFound; <- redirekts klienta pusē uz not-found.html
                context.Result = new JsonResult(new
                {
                    Message = "Objekts neeksistē",
                    Errors = new[] { context.Exception.Message }
                });
            }
            else if (context.Exception is BadRequestException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new
                {
                    Message = "Pieprasījuma kļūda",
                    Errors = new[] { context.Exception.Message }
                });
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;//.Unauthorized;// <- redirekts klienta pusē uz unauthorized.html
                context.Result = new JsonResult(new
                {
                    Message = "Liegta piekļuve",
                    Errors = new[] { context.Exception.Message }
                });
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //context.Result = new JsonResult(new
                //{
                //    error = new[] { context.Exception.Message },
                //    stackTrace = context.Exception.StackTrace
                //});
                context.Result = new JsonResult(new
                {
                    Message = "Servera iekšējā kļūda",
                    Errors = new[] { context.Exception.Message }
                });
            }
        }
    }
}
