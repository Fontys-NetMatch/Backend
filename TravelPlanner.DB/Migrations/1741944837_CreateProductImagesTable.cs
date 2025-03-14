using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Migrations;

public class ProductImagesTable : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductImage>(tableOptions: TableOptions.CheckExistence);

        var command = dbContext.CreateCommand();

        command.CommandText = @"
            ALTER TABLE ProductImages
            ADD CONSTRAINT FK_ProductImages_Products
            FOREIGN KEY (Product_ID)
            REFERENCES Products(ID)";
        command.ExecuteNonQuery();

    }
}