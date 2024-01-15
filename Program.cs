using System.Text.Json.Serialization;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddDbContext<TrippieContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("TrippieApiDbConnectionString"))
);
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<TrippieContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IPoiApiService, PoiApiService>();
builder.Services.AddTransient<ITripGenerationService, TripGenerationService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITripService, TripService>();

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