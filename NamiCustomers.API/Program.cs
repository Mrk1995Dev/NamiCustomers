using NamiCustomers.API.Extensions;


var builder = WebApplication.CreateBuilder(args);


builder.Services.BaseConfigures(builder);

var app = builder.Build();
app.BaseAppUse();

app.Run();



