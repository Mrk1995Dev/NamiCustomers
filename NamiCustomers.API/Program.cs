

using NamiCustomers.API;
using NamiCustomers.API.Middlewares;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddJsonFile("hostsettings.json", optional: true, reloadOnChange: true);

// Add Extention to the builder
builder.Services.ConfigurationAppSettings(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.ConfigureRateLimiting(builder.Configuration);
builder.Services.ConfigureApiVersioning(builder.Configuration);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigurationApplicationServices(builder.Configuration);
builder.Services.ConfigureDatabaseConnection(builder.Configuration);
builder.Services.ConfigurationSerilog(builder.Configuration);

builder.Services.AddHealthChecks()
    .AddDbContextCheck<DbContext>();

builder.Services.AddAuthentication();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDistributedMemoryCache();

builder.Services.AddOptions();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
  options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
);

builder.Services.AddHttpClient();
builder.Services.AddHttpLogging(o => { });
builder.Services.AddSerilog();
builder.Host.UseSerilog();
var app = builder.Build();
app.UseHttpLogging();

app.UseHealthChecks("/health", new HealthCheckOptions
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

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.InjectStylesheet("/css/swagger.css");
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean Api v1.0");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Clean Api v2.0");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
