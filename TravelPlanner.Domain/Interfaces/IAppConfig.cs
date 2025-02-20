using TravelPlanner.Domain.Models;

namespace TravelPlanner.Domain.Interfaces;

public interface IAppConfig
{

    public DbConfig GetDbConfig();

}