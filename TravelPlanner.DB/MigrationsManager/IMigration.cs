namespace TravelPlanner.DB.MigrationsManager;

public interface IMigration
{

    public void Up(DbContext dbContext);

}