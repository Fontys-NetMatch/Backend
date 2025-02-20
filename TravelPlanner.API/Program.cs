using System.Text;
using LinqToDB.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TravelPlanner.API;
using TravelPlanner.API.Controllers;
using TravelPlanner.API.Database;
using TravelPlanner.API.Infrastructure.Middleware;
using TravelPlanner.DB;
using TravelPlanner.DB.MigrationsManager;
using TravelPlanner.Domain.Interfaces;
using TravelPlanner.Domain.Models;

var builder = WebApplication.CreateBuilder(args);
// dev mode is enabled if the appsettings.Development.json file exists
var devMode = File.Exists("appsettings.Development.json");

if (devMode)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("-------------------------------------");
    Console.WriteLine("   App Running In Development Mode   ");
    Console.WriteLine("-------------------------------------");
    Console.ForegroundColor = ConsoleColor.White;
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
    // TODO: Implement JWT Bearer token security
    options.AddSecurityDefinition("JTW", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme.<br> 
                      Enter 'Bearer' [space] and then your token in the text input below.<br>
                      Example: 'Bearer r348h9hdqfd8gfd'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
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
builder.Configuration.GetSection("DbManager").Bind(dbConfig);

// Setup dependency injection
var config = new AppConfig(appUrl, allowedOrigins, dbConfig);
builder.Services.Add(new ServiceDescriptor(typeof(IAppConfig), config));

// Setup database
DataConnection.DefaultSettings = new DbSettings(config);
var migrationManager = new MigrationManager();
migrationManager.Init();

// Register services
builder.Services.AddTransient<DbContext>();
builder.Services.AddTransient<DbManager>();
builder.Services.AddTransient<StatusController>();
builder.Services.AddTransient<AuthController>();

// Auth
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key"))
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