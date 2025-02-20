using TravelPlanner.Domain.Interfaces;

namespace TravelPlanner.Domain.Models;

public class AppConfig : IAppConfig
{

    private readonly string _appUrl;
    private readonly string[] _allowedOrigins;
    private readonly DbConfig _dbConfig;

    public AppConfig(string appUrl, string allowedOrigins, DbConfig dbConfig)
    {
        _appUrl = appUrl;
        _allowedOrigins = allowedOrigins.Split(",");
        _dbConfig = dbConfig;
    }

    public string GetAppUrl()
    {
        return _appUrl;
    }

    public string[] GetAllowedOrigins()
    {
        return _allowedOrigins;
    }

    public DbConfig GetDbConfig()
    {
        return _dbConfig;
    }

}