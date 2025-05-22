using LinqToDB;
using LinqToDB.Data;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Migrations
{
    public class CreateProductSubTitleTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<ProductSubTitle>(tableOptions: TableOptions.CheckExistence);

            ((DataConnection)dbContext).Execute(@"
                ALTER TABLE ProductSubTitles
                ADD CONSTRAINT FK_ProductSubTitles_TransportTypeValues
                FOREIGN KEY (TransportTypeId) REFERENCES TransportTypeValues(Id)
            ");
        }
    }
}
