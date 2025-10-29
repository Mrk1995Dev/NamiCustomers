

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.Net;

namespace NamiCustomers.MVC.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,IWebHostEnvironment env)
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
            context.Response.ContentType = "text/html";

            if (!env.IsDevelopment())
            {
                // Show detailed error in development
                await context.Response.WriteAsync($@"
                <html>
                    <head><title>Error</title></head>
                    <body>
                        <h1>Error: {exception.Message}</h1>
                        <pre>{exception.StackTrace}</pre>
                    </body>
                </html>");
            }
            else
            {
                context.Session.SetString("Error", exception.Message);
                // Redirect to error page in production
                context.Response.Redirect($"/Home/Error?statusCode=500&message={exception.Message}");
            }



            //context.Response.ContentType = "application/json";
            //context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //logger.LogError($"شرح خطا :{Environment.NewLine} {exception.StackTrace}");
            //if (exception is NullReferenceException)
            //{
            //    exception = new Exception("اطلاعات دریافتی ناقص است");
            //}
            //context.Session.SetString("Error", exception.Message);
            //await context.Response.WriteAsync(exception.Message);
        }
    }
}