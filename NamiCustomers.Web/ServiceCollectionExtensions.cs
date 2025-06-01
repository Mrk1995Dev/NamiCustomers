using NamiCustomers.Web.Handlers;
using NamiCustomers.Web.Services.Auth.AuthServices;
using NamiCustomers.Web.Services.Auth.TokenServices;
using NamiCustomers.Web.Services.CustomerService.Implementation;
using NamiCustomers.Web.Services.LocalStorage;

namespace NamiCustomers.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<JwtAuthorizationMessageHandler>();

            services.AddHttpClient("ApiWithAuth", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7061/api/v1/");

            }).AddHttpMessageHandler<JwtAuthorizationMessageHandler>();


            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7061/api/v1/");
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();



            services.AddScoped<ICustomerService, CustomerService>(sp =>
            {
                var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
                return new CustomerService(httpClient);
            });      
 
 

            return services;
        }
    }
}
