using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddTransient<IPoiApiService, PoiApiService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();
app.UseHttpsRedirection();

// Run the app

app.Run();