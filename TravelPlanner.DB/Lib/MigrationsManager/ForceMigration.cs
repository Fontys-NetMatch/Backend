namespace TravelPlanner.DB.Lib.MigrationsManager;

[AttributeUsage(AttributeTargets.Class)]
public class ForceMigration : Attribute
{

    public ForceMigration()
    {

    }

}