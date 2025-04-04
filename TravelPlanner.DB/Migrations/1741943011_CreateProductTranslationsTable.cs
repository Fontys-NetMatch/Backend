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
            "ID"
        );
        DbUtils.GenerateUniqueConstraint(
            dbContext,
            "ProductTranslations",
            "LangIsoCode"
        );

    }
}