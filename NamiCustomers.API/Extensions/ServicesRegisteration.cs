using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NamiCustomers.Domain.Entities.Account;
using Serilog;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json;
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
            ;

        services.ConfigurationApplicationServices(configuration);

        return services;
    }



    private static IServiceCollection ConfigureOther(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
        services.AddIdentity<Domain.Entities.Account.ApplicationUser, ApplicationRole>()
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
                Description = "",
                Contact = new OpenApiContact
                {
                    Name = "Ali Moradi",
                    Email = "a.moradi@namikhodro.com",
                    Url = new Uri("https://www.linkedin.com/in/alimoradi573/")
                }
            });
            c.SwaggerDoc($"v2", new OpenApiInfo
            {
                Title = "Nami.Customers",
                Version = "v2.0",
                Description = ""
            });

            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "توکن بازگشتی از متد لاگین رو در کادر مربوطه کپی کنید:",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Please insert JWT into field",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }

    });





            // Enable the "Authorize" button in Swagger UI
            //c.OperationFilter<SwaggerAuthorizeOperationFilter>();TODO moradi thi not work


            // using System.Reflection;
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

        });
        return services;
    }

    private static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JWTSettings");
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
            };
        });
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
