using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NamiCustomers.Infrastucture.ExternalServices.Email.Dtos;
using NamiCustomers.Infrastucture.ExternalServices.SmsServices.Dtos;
using NamiCustomers.MVC.Handlers;
using NamiCustomers.MVC.Services.Account;
using NamiCustomers.MVC.Services.Auth;
using NamiCustomers.MVC.Services.Subscribers;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;

namespace NamiCustomers.MVC;

public static class ServicesRegisteration
{
    public static IServiceCollection BaseConfigures(
        this IServiceCollection services,
       WebApplicationBuilder webApplicationBuilder)
    {
        var configuration = webApplicationBuilder.Configuration;
        services
            .ConfigureAppSettings(configuration)
            .ConfigureCors()
            .ConfigureCurrentUser()
            .ConfigureMemoryCache()
            .ConfigureOther()
            .AddApplicationServices()
            .AddAuthentication(configuration)
            .ConfigureCookies()
            ;

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services,IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JWTSettings");

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        //options.Cookie.SameSite = SameSiteMode.Lax;
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.LoginPath = "/Account/LoginByMobile";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.SlidingExpiration = true;
    });
        return services;
    }
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<JwtAuthorizationMessageHandler>();

        services.AddHttpClient("ApiWithAuth", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7061/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        }).AddHttpMessageHandler<JwtAuthorizationMessageHandler>();


        services.AddHttpClient<IAuthService, AuthService>(client =>
        {
            //  client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
            client.BaseAddress = new Uri("https://localhost:7061/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ISubscriberService, SubscriberService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
            return new SubscriberService(httpClient);
        });

        services.AddScoped<IAccountService, AccountService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
            return new AccountService(httpClient);
        });



        return services;
    }

    private static IServiceCollection ConfigureOther(this IServiceCollection services)
    {
        services.AddHttpClient();
        services.AddHttpLogging(o => { });
        services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        services.AddScoped<ServerAuthenticationStateProvider, CustomAuthenticationStateProvider>();
        return services;
    }

    private static IServiceCollection ConfigureMemoryCache(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();
        return services;
    }
    private static IServiceCollection ConfigureCurrentUser(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddSession();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddSingleton<ICurrentUser, CurrentUser>();

        return services;
    }


    private static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.Configure<List<SmsSetting>>(configuration.GetSection("SmsSettings"));
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
        return services;
    }
 
    private static IServiceCollection ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
        });
    private static IServiceCollection ConfigureCookies(this IServiceCollection services)
    {
        services.ConfigureApplicationCookie(option =>
              {
                  // cookie setting
                  option.Cookie.Name = "MyCookie";
                  option.ExpireTimeSpan = TimeSpan.FromMinutes(10);

                  option.LoginPath = "/account/login";
                  option.AccessDeniedPath = "/account/AccessDenied";
                  option.SlidingExpiration = true;
              });
        return services;
    }
  
   
    
}
