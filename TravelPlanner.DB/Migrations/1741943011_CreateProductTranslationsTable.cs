using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Migrations;

public class CreateProductTranslationsTable : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductTranslation>(tableOptions: TableOptions.CheckExistence);

        DbUtils.GenerateForeignKey(
            dbContext,
            "ProductTranslations",
            "Products",
            "Id"
        );
        // TODO: Make this unique key use langisocode and productid
        DbUtils.GenerateUniqueConstraint(
            dbContext,
            "ProductTranslations",
            ["ProductId", "LangIsoCode"]
        );
        DbUtils.AssignDefaultValue(
            dbContext,
            "ProductTranslations",
            "IsActive",
            true
        );

    }
}