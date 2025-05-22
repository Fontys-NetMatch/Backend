using LinqToDB;
using LinqToDB.Data;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Transport;

namespace TravelPlanner.DB.Migrations
{
    public class CreateTransportSegmentTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<TransportSegment>(tableOptions: TableOptions.CheckExistence);
        }
    }
}