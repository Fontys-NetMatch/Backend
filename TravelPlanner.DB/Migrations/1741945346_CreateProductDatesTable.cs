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

        var command = dbContext.CreateCommand();

        command.CommandText = @"
            ALTER TABLE ProductDates
            ADD CONSTRAINT FK_ProductDates_Products
            FOREIGN KEY (Product_ID)
            REFERENCES ProductDates(ID)";
        command.ExecuteNonQuery();

    }
}