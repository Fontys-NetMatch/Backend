using LinqToDB;
using LinqToDB.Configuration;
using TravelPlanner.Domain.Interfaces;

namespace TravelPlanner.DB;

public class DbSettings : ILinqToDBSettings
{

    private readonly IAppConfig _config;

    public IEnumerable<IDataProviderSettings> DataProviders
        => [];

    public string DefaultConfiguration => "TravelAppDbConfig";
    public string DefaultDataProvider  => ProviderName.MySql;

    public IEnumerable<IConnectionStringSettings> ConnectionStrings
    {
        get
        {
            // note that you can return multiple ConnectionStringSettings instances here
            yield return
                new ConnectionStringSettings(
                    DefaultConfiguration,
                    _config.GetDbConfig().ConnectionString,
                    DefaultDataProvider
                );
        }
    }

    public DbSettings(IAppConfig config)
    {
        _config = config;
    }
    
}