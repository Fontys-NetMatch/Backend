using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreateDayPrograms : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<DayProgram>(tableOptions: TableOptions.CheckExistence);

            DbUtils.GenerateForeignKey(
                dbContext,
                "DayPrograms",     
                "CoordinatesId",     
                "GeoCoordinates",  
                "Id"                 
            );


            DbUtils.GenerateForeignKey(
                dbContext,
                "MediaContexts",
                "DayProgramId",
                "DayPrograms",
                "Id"
            );
        }
    }
}
