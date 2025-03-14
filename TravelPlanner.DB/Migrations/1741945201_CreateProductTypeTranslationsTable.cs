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

        var command = dbContext.CreateCommand();

        command.CommandText = @"
            ALTER TABLE ProductTypeTranslations
            ADD CONSTRAINT FK_ProductTypeTranslations_ProductTypes
            FOREIGN KEY (ProductType_ID)
            REFERENCES Products(ID)";
        command.ExecuteNonQuery();

    }
}