using LinqToDB.Data;

namespace TravelPlanner.DB;

public class DbContext : DataConnection
{

    public DbContext() : base("TravelAppDbConfig") { }

}