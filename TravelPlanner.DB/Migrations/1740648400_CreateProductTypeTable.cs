using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.DB.Migrations;

public class CreateProductType : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<ProductType>(tableOptions: TableOptions.CheckExistence);

<<<<<<< Updated upstream
        DbUtils.AssignDefaultValue(
            dbContext,
            "ProductTypes",
            "IsActive",
            true
        );
=======
        var Types = Enum.GetValues(typeof(ProductType))    
                            .Cast<ProductType>()
                            .Select(e => new ProductTypeEntity
                            {
                                Id = (int)e,
                                Name = e.ToString()
                            });

        foreach (var type in Types)
        {
            dbContext.Insert(type);
        }
>>>>>>> Stashed changes
    }
}