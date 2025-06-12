using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Enums;
using TravelPlanner.Domain.Models.Entities.Transport;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations
{
    public class CreateTransportTypeValueTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<TransportTypeValueEntity>(tableOptions: TableOptions.CheckExistence);

            var types = Enum.GetValues(typeof(TransportTypeValue))
                            .Cast<TransportTypeValue>()
                            .Select(e => new TransportTypeValueEntity
                            {
                                Id = (int)e,
                                Name = e.ToString()
                            });

            foreach (var type in types)
            {
                dbContext.InsertOrReplace(type);
            }
        }
    }
}
