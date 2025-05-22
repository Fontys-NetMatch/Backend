using LinqToDB;
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

            DbUtils.GenerateForeignKey(
                dbContext,
                "ProductSubTitles",
                "TransportTypeValueId",
                "TransportTypeValues",
                "Id"
            );
        }
    }
}
