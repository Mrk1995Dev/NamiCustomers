using NamiCustomers.Web.Components;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;
using NamiCustomers.Web.Models.Settings;
using NamiCustomers.Web.Services.CustomerService.Implementation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<EndPointSetting>(builder.Configuration.GetSection("EndPointSetting"));
// Add services to the container.
builder.Services.AddSingleton<ISettingFacade, SettingFacade>();
builder.Services.AddTransient<CustomerService>();
builder.Services.AddHttpClient();
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
