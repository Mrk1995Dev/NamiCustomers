using NamiCustomers.MVC;
using NamiCustomers.MVC.Extensions;
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
app.BaseAppUse(app);
app.Run();

