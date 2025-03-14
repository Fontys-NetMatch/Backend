using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations;

public class CreateProductTranslationsTable : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductTranslation>(tableOptions: TableOptions.CheckExistence);

        var command = dbContext.CreateCommand();

        command.CommandText = @"
            ALTER TABLE ProductTranslations
            ADD CONSTRAINT FK_ProductTranslations_Products
            FOREIGN KEY (Product_ID)
            REFERENCES Products(ID)";
        command.ExecuteNonQuery();

    }
}