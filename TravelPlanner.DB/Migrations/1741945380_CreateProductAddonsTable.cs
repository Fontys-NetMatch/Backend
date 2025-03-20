using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Product;
using TravelPlanner.Domain.Models.Entities.Products;

namespace TravelPlanner.DB.Migrations;

public class CreateProductAddons : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductAddon>(tableOptions: TableOptions.CheckExistence);
    }
}