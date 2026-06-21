using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using NamiCustomers.Web;
using NamiCustomers.Web.Services.Auth;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var environment = builder.HostEnvironment.Environment;
builder.Configuration.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false);
var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("AdminAccess", policy => policy.RequireRole("Admin"));
    // Policy that requires any of these roles
    options.AddPolicy("OperatorAccess", policy =>
        policy.RequireRole("Admin", "Operator"));

    // Policy that requires all specified roles
    options.AddPolicy("SubscriberAccess", policy =>
        policy.RequireRole("Admin")
              .RequireRole("PowerUser")
              .RequireRole("subscriber")
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
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddApplicationServices(baseUrl);
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 3500;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Outlined;
});

await builder.Build().RunAsync();
