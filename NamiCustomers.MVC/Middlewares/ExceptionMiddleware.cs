

using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace NamiCustomers.MVC.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError($"خطا در سامانه رخ داده است  :{Environment.NewLine} {ex} {Environment.NewLine}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //await context.Response.WriteAsync(new ResultDto(
            // exception.Message,
            // false
            //).ToString());
            logger.LogError($"شرح خطا :{Environment.NewLine} {exception.StackTrace}");
            if (exception is NullReferenceException)
            {
                exception = new Exception("اطلاعات دریافتی ناقص است");
            }
            context.Session.SetString("Error", exception.Message);
            //await context.Response.WriteAsync(exception.Message);
        }
    }
}