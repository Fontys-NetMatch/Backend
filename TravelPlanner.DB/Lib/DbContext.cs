using LinqToDB.Data;

namespace TravelPlanner.DB.Lib;

public class DbContext : DataConnection
{

    public DbContext() : base("TravelAppDbConfig") { }

}