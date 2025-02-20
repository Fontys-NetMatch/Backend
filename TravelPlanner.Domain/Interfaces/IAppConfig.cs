using TravelPlanner.Domain.Models;

namespace TravelPlanner.Domain.Interfaces;

public interface IAppConfig
{

    public string GetAppUrl();
    public string[] GetAllowedOrigins();
    public DbConfig GetDbConfig();
    public JwtConfig GetJwtConfig();

}