using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Facility;

namespace TravelPlanner.DB.Migrations
{
    public class CreateFacilityInformationTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<FacilityInformation>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
