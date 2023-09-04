using System.Text;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using MongoDB.Driver;
using RentApp.Auth;
using RentApp.Collection;
using RentApp.Models;
using RentApp.Repository;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();
// Add services to the container.
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings")
);
/*
builder.Services.AddSingleton<IMongoDatabase>(options =>
{
    var settings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});
var jwtSecret = builder.Configuration["JwtSecret"];
var key = Encoding.ASCII.GetBytes(jwtSecret);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
    */

builder.Services.AddSingleton<IPeopleRepository, PeopleRepository>();
builder.Services.AddSingleton<IRealEstateRepository, RealEstateRepository>();
builder.Services.AddScoped<IRealEstateRepository, RealEstateRepository>();
builder.Services.AddLogging();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();


app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();