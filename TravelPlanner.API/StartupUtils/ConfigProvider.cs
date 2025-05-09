using TravelPlanner.Domain.Interfaces;
using TravelPlanner.Domain.Models;

namespace TravelPlanner.API.StartupUtils;

public static class ConfigProvider
{

    public static void LoadFiles(WebApplicationBuilder builder)
    {
        // dev mode is enabled if the appsettings.dev.json file exists
        var b = File.Exists("appsettings.dev.json");

        if (b)
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
        builder.Configuration.AddJsonFile(b ? "appsettings.dev.json" : "appsettings.json", false, true);
    }

    public static AppConfig Register(WebApplicationBuilder builder)
    {
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
        var config = new AppConfig(appUrl, allowedOrigins, dbConfig, jwtConfig, builder.Environment.IsDevelopment());
        builder.Services.Add(new ServiceDescriptor(typeof(IAppConfig), config));
        return config;
    }

}