using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Migrations;

public class CreateProductTypesTranslations : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductTypeTranslation>(tableOptions: TableOptions.CheckExistence);

        DbUtils.GenerateForeignKey(
            dbContext,
            "ProductTypeTranslations",
            "ProductTypes",
            "ID"
        );
        DbUtils.AssignDefaultValue(
            dbContext,
            "ProductTypeTranslations",
            "IsActive",
            true
        );

    }
}