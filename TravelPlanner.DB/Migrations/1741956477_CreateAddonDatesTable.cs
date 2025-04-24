using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Models.Entities.Products;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreateAddonDatesTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<AddonDate>(tableOptions: TableOptions.CheckExistence);

            DbUtils.GenerateForeignKey(
                dbContext,
                "AddonDates",
                "ProductDates",
                "Id"
            );
            DbUtils.GenerateForeignKey(
                dbContext,
                "AddonDates",
                "ProductAddons",
                "Id"
            );

        }
    }
}
