using NamiCustomers.MVC;
using NamiCustomers.MVC.Filters;
using NamiCustomers.MVC.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{

    options.Filters.Add<VinFilter>();//NOTICE:  Alternatively, you can apply it to a specific controller or action method by using the ServiceFilter attribute
    //options.Filters.Add(new AuthorizeFilter());
});

builder.Services.BaseConfigures(builder);
//builder.Services.AddScoped<CustomAuthorizeAttribute>();
var app = builder.Build();

// Exception middleware should be at the TOP of the pipeline Your custom middleware
app.UseMiddleware<ExceptionMiddleware>();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
// Add session middleware HERE (after UseRouting and before MapControllerRoute)
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                 );
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

