using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.New_Models.Entities.Product;

namespace TravelPlanner.DB.Migrations
{
    public class CreateProductCharacteristicsTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<ProductCharacteristics>(tableOptions: TableOptions.CheckExistence);
        }
    }
}
