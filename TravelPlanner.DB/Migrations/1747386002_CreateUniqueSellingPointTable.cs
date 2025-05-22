using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities.UniqueSellingPoints;

namespace TravelPlanner.DB.Migrations
{
    public class CreateUniqueSellingPointTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<UniqueSellingPoint>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
