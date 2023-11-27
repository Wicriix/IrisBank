using Iris.Commons.Library.Base;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Iris.Commons.Library.Middelwares
{
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
            // Log issues and handle exception response
            string result = string.Empty;
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            if (exception.GetType() == typeof(ValidationException))
            {
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(((ValidationException)exception).Errors.Select(e => new GenericResponse { OperationSuccess= false, ErrorMessage = "PropertyName" + e.PropertyName + "ErrorMessage" + e.ErrorMessage }));
            }
            else
            {
                result = JsonConvert.SerializeObject(new GenericResponse { OperationSuccess = false, ErrorMessage = exception.Message });
            }
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
