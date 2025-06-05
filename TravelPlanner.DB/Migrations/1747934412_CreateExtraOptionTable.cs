using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreateExtraOptionTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<ExtraOption>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
