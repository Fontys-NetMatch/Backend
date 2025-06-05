using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreateErrataTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<Errata>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
