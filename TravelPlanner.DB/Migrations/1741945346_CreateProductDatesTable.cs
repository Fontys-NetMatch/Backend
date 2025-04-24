using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Migrations;

public class CreateProductDatesTable : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductDate>(tableOptions: TableOptions.CheckExistence);

        DbUtils.GenerateForeignKey(
            dbContext,
            "ProductDates",
            "Products",
            "Id"
        );
        DbUtils.AssignDefaultValue(
            dbContext,
            "ProductDates",
            "IsActive",
            true
        );

    }
}