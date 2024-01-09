using Entities;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddDbContext<TrippieContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("TrippieApiDbConnectionString"))
);
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<TrippieContext>();

builder.Services.AddTransient<IPoiApiService, PoiApiService>();
builder.Services.AddTransient<ITripGenerationService, TripGenerationService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.MapIdentityApi<User>();
app.MapControllers();
app.UseHttpsRedirection();

// Run the app

app.Run();