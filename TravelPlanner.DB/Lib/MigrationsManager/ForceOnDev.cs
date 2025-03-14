namespace TravelPlanner.DB.Lib.MigrationsManager;

[AttributeUsage(AttributeTargets.Class)]
public class ForceOnDev : Attribute
{

    public ForceOnDev()
    {

    }

}