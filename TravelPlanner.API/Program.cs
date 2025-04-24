using LinqToDB.Data;
using TravelPlanner.API.Infrastructure.Middleware;
using TravelPlanner.API.StartupUtils;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;

var builder = WebApplication.CreateBuilder(args);
ConfigProvider.LoadFiles(builder);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
SwaggerProvider.Register(builder.Services);

// Load Config
var config = ConfigProvider.Register(builder);

// LoadFiles services
ServicesProvider.Register(builder.Services);

// Setup database
DataConnection.DefaultSettings = new DbSettings(config);
var migrationManager = new MigrationManager();
migrationManager.RegisterCustomSchemas();
migrationManager.Init(config);

// Auth
AuthProvider.Register(builder.Services, config);

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

Router.Register(app);

app.Run();