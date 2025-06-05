using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreateExtraInformationTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<ExtraInformation>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
