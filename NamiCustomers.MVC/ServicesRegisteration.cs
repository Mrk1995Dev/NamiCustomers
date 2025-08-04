using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using NamiCustomers.Infrastucture.ExternalServices.Email.Dtos;
using NamiCustomers.MVC.Handlers;
using NamiCustomers.MVC.Services;
using NamiCustomers.MVC.Services.Auth;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace NamiCustomers.MVC;

public static class ServicesRegisteration
{
    public static IServiceCollection BaseConfigures(
        this IServiceCollection services,
       WebApplicationBuilder webApplicationBuilder)
    {
        var configuration = webApplicationBuilder.Configuration;
        services
            //.ConfigureAppSettings(configuration)
            .ConfigureCors()
            .ConfigureCurrentUser()
            .ConfigureMemoryCache()
            .ConfigureOther()
            .AddApplicationServices(configuration)
            .AddAuthentication(configuration)
            .AddAuthorization(configuration)
            .ConfigureCookies()
            ;

        return services;
    }

    public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
        });

        services.AddAuthorization(options =>
{
    options.AddPolicy(MyPloicies.AdminAccess, policy => policy.RequireRole(MyRoles.Admin));
 
    options.AddPolicy(MyPloicies.OperatorAccess, policy =>
    {
        policy.RequireRole(MyRoles.Admin);
        policy.RequireRole(MyRoles.Operator);
    }
       );
 
    options.AddPolicy(MyPloicies.SubscriberAccess, policy => {
        policy.RequireRole(MyRoles.Subscriber);
    }
              );
    //options =>
    //{
    //    // Dynamic permission policies
    //    options.AddPolicy("Permission", policy =>
    //        policy.RequireAssertion(context =>
    //            context.User.HasClaim(c =>
    //                c.Type == "Permission" &&
    //                c.Value == context.GetRequiredService<IAuthorizationService>()
    //                    .GetPolicyRequirements().First().ToString())));

    //    // Specific permission policies
    //    options.AddPolicy("CanEditProducts", policy =>
    //        policy.RequireClaim("Permission", "products.edit"));
    //    options.AddPolicy("CanDeleteUsers", policy =>
    //        policy.RequireClaim("Permission", "users.delete"));
    //}

});
        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
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
    }) ;
        return services;
    }
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<JwtAuthorizationMessageHandler>();

        services.AddHttpClient("ApiWithAuth", client =>
        {
            client.BaseAddress = new Uri(configuration["EndPointSetting:BaseAddress"]);
            // client.BaseAddress = new Uri("https://localhost:7061/api/v1/");
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        }).AddHttpMessageHandler<JwtAuthorizationMessageHandler>();


        services.AddHttpClient<IAuthService, AuthService>(client =>
        {
            client.BaseAddress = new Uri(configuration["EndPointSetting:BaseAddress"]);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddScoped<ITokenSessionService, TokenSessionService>();
        services.AddScoped<ISubscriberService, SubscriberService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            return new SubscriberService(httpClient, httpContextAccessor);
        });
        services.AddScoped<IVehicleService, VehicleService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var subscriberService = sp.GetRequiredService<ISubscriberService>();
            return new VehicleService(httpClient, httpContextAccessor, subscriberService);
        });


        services.AddScoped<IAccountService, AccountService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
            return new AccountService(httpClient);
        });
        services.AddScoped<IRoleService, RoleService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
            return new RoleService(httpClient);
        });
        services.AddScoped<IUserService, UserService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            return new UserService(httpClient);
        });
        services.AddScoped<IDealerService, DealerService>(sp =>
        {
            var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiWithAuth");
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            var subscriberService = sp.GetRequiredService<ISubscriberService>();
            return new DealerService(httpClient, subscriberService);
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
        services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();
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
