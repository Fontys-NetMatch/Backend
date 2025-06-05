using LinqToDB;
using LinqToDB.Data;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities.Transport;

namespace TravelPlanner.DB.Migrations
{
    public class CreateTransportInformationTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<TransportInformation>(tableOptions: TableOptions.CheckExistence);

            ((DataConnection)dbContext).Execute(@"
                ALTER TABLE TransportInformations
                ADD CONSTRAINT FK_TransportInformations_TransportType
                FOREIGN KEY (TransportTypeId) REFERENCES TransportTypeValues(Id)
             ");

        }
    }
}
