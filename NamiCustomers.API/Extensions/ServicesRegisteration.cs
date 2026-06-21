using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using NamiCustomers.Abstractions.Dtos.Settings;
using NamiCustomers.API.Services.Validator;
using NamiCustomers.Application.Mappings;
using NamiCustomers.Domain.Entities.Account;
using Serilog;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;


namespace NamiCustomers.API.Extensions;

public static class ServicesRegisteration
{
    public static IServiceCollection BaseConfigures(
        this IServiceCollection services,
       WebApplicationBuilder webApplicationBuilder)
    {
        var configuration = webApplicationBuilder.Configuration;
        services
            .ConfigureAppSettings(configuration)
            .ConfigureSerilog(configuration, webApplicationBuilder)
            .ConfigureApiVersioning(configuration)
            .ConfigureDatabaseConnection(configuration)
            .ConfigureCors()
            .ConfigureSwagger()
            .ConfigureJwt(configuration)
            .ConfigureRateLimiting(configuration)
            .ConfigureControllers()
            .ConfigureCurrentUser()
            .ConfigureHealthcheck()
            .ConfigureMemoryCache()
            .ConfigureOther()
             .AddAuthorization(configuration)
               .ConsolEnvironment()
            ;
        services.ConfigurationApplicationServices(configuration);

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

            options.AddPolicy(MyPloicies.SubscriberAccess, policy =>
            {
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

    private static IServiceCollection ConsolEnvironment(this IServiceCollection services)
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        Console.WriteLine(environmentName);
        return services;
    }

    private static IServiceCollection ConfigureOther(this IServiceCollection services)
    {
        services.AddAutoMapper(c => { c.AddProfile(typeof(GeneralProfile)); }, AppDomain.CurrentDomain.GetAssemblies());

        services.AddHttpClient();
        services.AddHttpLogging(o => { });
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
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        //services.AddSingleton<ICurrentUser, CurrentUser>();

        return services;
    }


    private static IServiceCollection ConfigureHealthcheck(this IServiceCollection services)
    {
        services.AddHealthChecks()
        .AddDbContextCheck<DbContext>();

        return services;
    }
    private static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            //options.ModelBinderProviders.Insert(0, new EnumDisplayNameModelBinderProvider());
        })
            .AddJsonOptions(options =>
           {
               options.JsonSerializerOptions.PropertyNamingPolicy = null; // PascalCase
               options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
           });

        services.AddEndpointsApiExplorer();
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
        services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
        return services;
    }

    private static IServiceCollection ConfigureSerilog(this IServiceCollection services, IConfiguration configuration, WebApplicationBuilder webApplicationBuilder)
    {
        Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
        services.AddSerilog();
        webApplicationBuilder.Host.UseSerilog();
        return services;

    }
    private static IServiceCollection ConfigureApiVersioning(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning()
            .AddApiExplorer(options =>
             {
                 options.GroupNameFormat = "'v'VVV";
                 options.SubstituteApiVersionInUrl = true;
                 options.DefaultApiVersion = new ApiVersion(1, 0);
                 options.AssumeDefaultVersionWhenUnspecified = true;
             });
        return services;
    }
    private static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Identity
        services.AddIdentity<Domain.Entities.Account.ApplicationUser, ApplicationRole>(options =>
        {
            options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
        })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddRoles<ApplicationRole>()
        ;

        //services.Configure<IdentityOptions>(option =>
        //{
        //    //UserSetting
        //    //option.User.AllowedUserNameCharacters = "abcd123";
        //    option.User.RequireUniqueEmail = true;

        //    //Password Setting
        //    option.Password.RequireDigit = false;
        //    option.Password.RequireLowercase = false;
        //    option.Password.RequireNonAlphanumeric = false;//!@#$%^&*()_+
        //    option.Password.RequireUppercase = false;
        //    option.Password.RequiredLength = 6;
        //    option.Password.RequiredUniqueChars = 1;

        //    //Lokout Setting
        //    option.Lockout.MaxFailedAccessAttempts = 3;
        //    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(10);

        //    //SignIn Setting
        //    option.SignIn.RequireConfirmedAccount = false;
        //    option.SignIn.RequireConfirmedEmail = false;
        //    option.SignIn.RequireConfirmedPhoneNumber = false;

        //});

        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("Web-db")));
        services.AddScoped<IAppDbContext, AppDbContext>();






        return services;
    }

    private static IServiceCollection ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()

            );
        });

    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {

        services.AddSwaggerGen(c =>
        {

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Nami.Customers",
                Version = "v1.0",
            });

            c.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "Nami.Customers",
                Version = "v2.0",

            });

            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            // Step 1: Define the security scheme (unchanged)
            // تعریف Bearer
            var securityScheme = new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Description = "فقط توکن رو وارد کن (بدون Bearer)",

            };
            c.AddSecurityDefinition("Bearer", securityScheme);


            // Step 2: اعمال طرح امنیتی global (تصحیح‌شده برای .NET 10 / v10)

            c.AddSecurityRequirement(r => new OpenApiSecurityRequirement
    {
        {
           new OpenApiSecuritySchemeReference(securityScheme.Scheme) // این کلاس وجود دارد!
          ,
            new List<string>()  // بدون scopes برای Bearer پایه
        }
    });

            // using System.Reflection;
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

        });
        return services;
    }

    private static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<ITokenValidator, TokenValidate>();


        var jwtSettings = configuration.GetSection("JWTSettings");
        services.AddAuthentication(options =>
        {
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    Console.WriteLine("Authentication failed: " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    //validate ekhtesasi
                    //log
                    var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidator>();
                    return tokenValidatorService.Execute(context);
                    //Console.WriteLine("Token validated for: " + context.Principal.Identity.Name);
                    //return Task.CompletedTask;
                },
                OnChallenge = context =>
                {
                    return Task.CompletedTask;
                }
               ,
                OnMessageReceived = context =>
                {
                    //زمانی که درخواستی دریافت کردم قبل از هر واقعه دیگری کار خاصی روی درخواست انجام دهم
                    return Task.CompletedTask;
                },
                OnForbidden = context =>
                {
                    return Task.CompletedTask;
                }
            };
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
            };
        })
        .AddCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.SameSite = SameSiteMode.Strict;
        });
        ;
        return services;
    }

    private static IServiceCollection ConfigureRateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

        services.AddOptions();
        services.AddMemoryCache();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();


        services.AddInMemoryRateLimiting();
        return services;
    }
}
