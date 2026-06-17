using NamiCustomers.API.Extensions;
using NamiCustomers.API.Middlewares;


var builder = WebApplication.CreateBuilder(args);
builder.Services.BaseConfigures(builder);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
// Program.cs - Register middleware correctly

app.BaseAppUse();

app.MapControllers();
app.Run();



