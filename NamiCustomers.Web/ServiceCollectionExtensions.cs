using NamiCustomers.Web.Handlers;
using NamiCustomers.Web.Services.Account.Contract;
using NamiCustomers.Web.Services.Auth.AuthServices.Contract;
using NamiCustomers.Web.Services.Auth.AuthServices.Implementation;
using NamiCustomers.Web.Services.Auth.TokenServices;
using NamiCustomers.Web.Services.CustomerService.Implementation;
using NamiCustomers.Web.Services.Dealer.Contract;
using NamiCustomers.Web.Services.Dealer.Implementation;
using NamiCustomers.Web.Services.LocalStorage;
using NamiCustomers.Web.Services.Subscriber.Contract;
using NamiCustomers.Web.Services.Vehicle.Contract;
using NamiCustomers.Web.Services.Vehicle.Implementation;

namespace NamiCustomers.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string baseUrl)
        {
            services.AddScoped<JwtAuthorizationMessageHandler>();
            services.AddScoped<VehicleContext>();

            services.AddHttpClient("ApiWithAuth", client =>
            {
                client.BaseAddress = new Uri(baseUrl);

            }).AddHttpMessageHandler<JwtAuthorizationMessageHandler>();


            services.AddHttpClient<IAuthService, AuthService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILocalStorageService, LocalStorageService>();



            services.AddScoped<ICustomerService, CustomerService>(sp =>
            {
                var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
                return new CustomerService(httpClient);
            });

            services.AddHttpClient<IVehicleService, VehicleService>("ApiWithAuth", configureClient =>
            {
                configureClient.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient<IDealerService, DealerService>("ApiWithAuth", configureClient =>
            {
                configureClient.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient<ISubscriberService, SubscriberService>("ApiWithAuth", configureClient =>
            {
                configureClient.BaseAddress = new Uri(baseUrl);
            });

            services.AddHttpClient<IAccountService, AccountService>("ApiWithAuth", configureClient =>
            {
                configureClient.BaseAddress = new Uri(baseUrl);
            });

            return services;
        }
    }
}
