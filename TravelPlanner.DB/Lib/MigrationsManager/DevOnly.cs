namespace TravelPlanner.DB.Lib.MigrationsManager;

[AttributeUsage(AttributeTargets.Class)]
public class DevOnly : Attribute
{

    public DevOnly()
    {

    }

}