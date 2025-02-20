using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Database;

public class DbManager : DbContext
{

    public ITable<User> Users => this.GetTable<User>();

}