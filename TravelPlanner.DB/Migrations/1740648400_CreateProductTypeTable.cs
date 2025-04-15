using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Migrations;

public class CreateProductType : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductType>(tableOptions: TableOptions.CheckExistence);

        DbUtils.AssignDefaultValue(
            dbContext,
            "ProductTypes",
            "IsActive",
            true
        );
    }
}