using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Migrations;

public class CreateProductAddonTranslations : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductAddonTranslation>(tableOptions: TableOptions.CheckExistence);

        var command = dbContext.CreateCommand();

        command.CommandText = @"
            ALTER TABLE ProductAddonTranslations
            ADD CONSTRAINT FK_ProductAddonTranslations_ProductAddons
            FOREIGN KEY (ProductAddon_ID)
            REFERENCES ProductAddons(ID)";
        command.ExecuteNonQuery();

    }
}