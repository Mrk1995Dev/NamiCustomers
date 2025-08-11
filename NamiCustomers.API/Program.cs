

using NamiCustomers.API;
using NamiCustomers.API.Extensions;
using NamiCustomers.API.Middlewares;
using NamiCustomers.Infrastucture;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


builder.Services.BaseConfigures(builder);

var app = builder.Build();
app.BaseAppUse();

app.Run();



