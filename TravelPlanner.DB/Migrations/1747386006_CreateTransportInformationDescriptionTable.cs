using LinqToDB;
using LinqToDB.Data;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities.Transport;

namespace TravelPlanner.DB.Migrations
{
    public class CreateTransportInformationDescriptionTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<TransportInformationDescription>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
