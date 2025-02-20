namespace TravelPlanner.DB.Lib.MigrationsManager;

public interface IMigration
{

    public void Up(DbContext dbContext);

}