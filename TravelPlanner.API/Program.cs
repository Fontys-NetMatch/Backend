using LinqToDB.Data;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.OpenApi.Models;
using TravelPlanner.API;
using TravelPlanner.API.Controllers;
using TravelPlanner.API.Infrastructure;
using TravelPlanner.API.Infrastructure.Middleware;
using TravelPlanner.BLL;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Interfaces;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
// dev mode is enabled if the appsettings.Development.json file exists
var devMode = File.Exists("appsettings.Development.json");

if (devMode)
{
    builder.Environment.EnvironmentName = "Development";

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("-------------------------------------");
    Console.WriteLine("   App Running In Development Mode   ");
    Console.WriteLine("-------------------------------------");
    Console.ForegroundColor = ConsoleColor.White;
}
else
{
    builder.Environment.EnvironmentName = "Production";
}

builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile(devMode ? "appsettings.Development.json" : "appsettings.json", false, true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "NetMatch - TravelPlanner API",
        Description = "The api for the TravelPlanner application",
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        BearerFormat = "JWT",
        Scheme = "bearer",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

// Load Config
// Load configuration
var appUrl = builder.Configuration.GetValue<string>("AppUrl");
var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins");

if (appUrl == null) throw new ArgumentNullException(appUrl);
if (allowedOrigins == null) throw new ArgumentNullException(allowedOrigins);

DbConfig dbConfig = new();
builder.Configuration.GetSection("Database").Bind(dbConfig);

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtSecret = jwtSection.GetValue<string>("Secret");
var jwtIssuer = jwtSection.GetValue<string>("Issuer");
var jwtAudience = jwtSection.GetValue<string>("Audience");

if (jwtSecret == null) throw new ArgumentNullException(jwtSecret);
if (jwtIssuer == null) throw new ArgumentNullException(jwtIssuer);
if (jwtAudience == null) throw new ArgumentNullException(jwtAudience);

JwtConfig jwtConfig = new()
{
    Secret = jwtSecret,
    Issuer = jwtIssuer,
    Audience = jwtAudience
};

// Setup dependency injection
var config = new AppConfig(appUrl, allowedOrigins, dbConfig, jwtConfig, devMode);
builder.Services.Add(new ServiceDescriptor(typeof(IAppConfig), config));

// Register services
builder.Services.AddTransient<DbContext>();
builder.Services.AddTransient<DbManager>();

builder.Services.AddTransient<StatusController>();
builder.Services.AddTransient<AuthController>();

builder.Services.AddSingleton<IAuthContainer, AuthContainer>();

// Setup database
DataConnection.DefaultSettings = new DbSettings(config);
var migrationManager = new MigrationManager();
migrationManager.Init(config);

// Auth
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = BearerTokenDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = BearerTokenDefaults.AuthenticationScheme;
}).AddBearerToken(options =>
{
    options.Events = new BearerTokenEvents
    {
        OnMessageReceived = context =>
        {
            JwtTokenValidator picturaApiKeyValidator = new(config);
            return picturaApiKeyValidator.VerifyToken(context);
        }
    };
});
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policyBuilder => policyBuilder
    .WithOrigins(config.GetAllowedOrigins())
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();

app.UseMiddleware<AuthErrorMiddleware>();
app.UseHttpsRedirection();

_ = new Router(app);

app.Run();