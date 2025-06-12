using System.Text;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Migrations;

public class CreateProductAddonTranslationsTable : IMigration
{

    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductAddonTranslation>(tableOptions: TableOptions.CheckExistence);

        DbUtils.GenerateForeignKey(
            dbContext,
            "ProductAddonTranslations",
            "ProductAddons",
            "Id"
        );
        DbUtils.AssignDefaultValue(
            dbContext,
            "ProductAddonTranslations",
            "IsActive",
            true
        );

    }

}