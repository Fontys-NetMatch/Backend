using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreateDistanceInformationTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<DistanceInformation>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
