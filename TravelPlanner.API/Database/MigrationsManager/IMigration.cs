namespace TravelPlanner.API.Database.MigrationsManager;

public interface IMigration
{

    public void Up(DbContext dbContext);

}