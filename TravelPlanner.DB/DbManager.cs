using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB;

public class DbManager : DbContext
{

    public ITable<User> Users => this.GetTable<User>();

}