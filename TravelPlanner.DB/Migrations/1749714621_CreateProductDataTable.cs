using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Migrations
{
    public class CreateProductDataTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<Productdata>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
