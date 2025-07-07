using HealthChecks.UI.Client;
using NamiCustomers.Abstractions.Dtos.Health;
using NamiCustomers.API.Middlewares;
using System.Reflection;

namespace NamiCustomers.API.Extensions
{
    public static class AppUseRegistration
    {
        public static IApplicationBuilder BaseAppUse(this IApplicationBuilder app)
        {
            app.UsingSwagger();
            app.UsingCors();
            app.UsingEndPoints();
            app.UsingMiddlewares();

            app.UseHttpLogging();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
           
           
            

            return app;
        }

        private static IApplicationBuilder UsingMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }

        private static IApplicationBuilder UsingSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
               
                c.InjectStylesheet("/css/swagger.css");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nami.Customers v1.0");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Nami.Customers v2.0");
                
            });
            return app;
        }

        private static IApplicationBuilder UsingCors(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            return app;
        }

        private static void UsingEndPoints(this IApplicationBuilder app)
        {
            app.UseRouting();
            //If there are calls to app.UseRouting() and app.UseEndpoints(...), the call to app.UseAuthorization() must go between them
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHealthChecks("/health", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    AllowCachingResponses = false,
                    ResponseWriter = async (context, report) =>
                    {
                        context.Response.ContentType = "application/json";
                        var response = new HealthCheckResponse
                        {
                            Status = report.Status.ToString(),
                            HealthChecks = report.Entries.Select(x => new IndividualHealthCheckResponse
                            {
                                Component = x.Key,
                                Status = x.Value.Status.ToString(),
                                Description = x.Value.Description

                            }),
                            HealthCheckDuration = report.TotalDuration
                        };
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                    }
                });

            });
        }
    }
}
