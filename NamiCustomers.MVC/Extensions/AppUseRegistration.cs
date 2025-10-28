//using HealthChecks.UI.Client;
//using Microsoft.AspNetCore.Diagnostics.HealthChecks;
//using NamiCustomers.Abstractions.Dtos.Health;
using NamiCustomers.MVC.Middlewares;
using Newtonsoft.Json;

namespace NamiCustomers.MVC.Extensions
{
    public static class AppUseRegistration
    {
        public static IApplicationBuilder BaseAppUse(this IApplicationBuilder app, WebApplication webApplication)
        {
            app.UsingApplicationBuilder(webApplication);//diablo

            return app;
        }
        private static IApplicationBuilder UsingMiddlewares(this IApplicationBuilder app)
        {
            app.UseSession();// Add session middleware HERE (after UseRouting and before MapControllerRoute)
                             //app.UseRotativa();but  i use middleware that focibly use it here 
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
        private static IApplicationBuilder UsingCors(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            return app;
        }
        private static void UsingApplicationBuilder(this IApplicationBuilder app, WebApplication webApplication)
        {
           
            app.UsingMiddlewares();// Exception middleware should be at the TOP of the pipeline Your custom middleware

            app.UseHttpLogging();
            app.UseHttpsRedirection();
        
            app.UsingCors();




            app.UseRouting();
            

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();//If there are calls to app.UseRouting() and app.UseEndpoints(...), the call to app.UseAuthorization() must go between them
           
            //app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                // Configure the HTTP request pipeline.
                if (!webApplication.Environment.IsDevelopment())
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseStatusCodePagesWithReExecute("/error/{0}");
                    app.UseHsts();
                }

                //endpoints.MapControllers();
                // endpoints.MapHealthChecks("/health", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

                //endpoints.MapHealthChecks("/health", new HealthCheckOptions
                //{
                //    AllowCachingResponses = false,
                //    ResponseWriter = async (context, report) =>
                //    {
                //        context.Response.ContentType = "application/json";
                //        var response = new HealthCheckResponse
                //        {
                //            Status = report.Status.ToString(),
                //            HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
                //            {
                //                Component = x.Key,
                //                Status = x.Value.Status.ToString(),
                //                Description = x.Value.Description

                //            }),
                //            HealthCheckDuration = report.TotalDuration
                //        };
                //        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                //    }
                //});

                webApplication.MapStaticAssets();

                webApplication.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                webApplication.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}").WithStaticAssets(); ;
                
                webApplication.MapControllerRoute(//NOTICE:this line for APIs
                    name: "gateway",
                    pattern: "api/{controller=Gateway}/{action=Get}/{id?}");

                //webApplication.MapHealthChecks("health", new HealthCheckOptions
                //{
                //    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                //});


            });




        }
    }
}
