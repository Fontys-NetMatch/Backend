using LinqToDB;
using LinqToDB.Data;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Database;

public class DbContext : DataConnection
{

    public DbContext() : base("TravelAppDbConfig") { }

    public ITable<User> Users => this.GetTable<User>();

}