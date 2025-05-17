using System.Reflection;
using System.Text;
using Asp.Versioning;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;

using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NamiCustomers.API
{
    public static class ServicesRegisteration
    {
        public static IServiceCollection ConfigurationAppSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<List<SmsSetting>>(configuration.GetSection("SmsSettings"));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<FavaWhiteListSettting>(configuration.GetSection("FavaWhiteListSettings"));
            return services;
        }

        public static IServiceCollection ConfigureNLog(this IServiceCollection services)
        {
            var config = new NLog.Config.XmlLoggingConfiguration("nlog.config");
            LogManager.Configuration = config;
            return services;
        }
        public static IServiceCollection ConfigurationSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
            return services;

        }
        public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services, IConfiguration configuration)
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
        public static IServiceCollection ConfigureDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SAMPLEConnection")));
            services.AddScoped<IAppDbContext, AppDbContext>();
            return services;
        }

        public static IServiceCollection ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            //services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Clean",
                    Version = "v1.0",
                    Description = ""
                });
                c.SwaggerDoc($"v2", new OpenApiInfo
                {
                    Title = "Clean",
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
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securityScheme, new string[] { }}
                });
                // using System.Reflection;
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            });
            return services;
        }

        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
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
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
                };
            });
            return services;
        }

        public static IServiceCollection ConfigureRateLimiting(this IServiceCollection services, IConfiguration configuration)
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
}
