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

            var command = dbContext.CreateCommand();

            command.CommandText = @"
            ALTER TABLE AddonDates
            ADD CONSTRAINT FK_AddonDates_ProductDates
            FOREIGN KEY (ProductDate_ID)
            REFERENCES ProductDates(ID)";
            command.ExecuteNonQuery();

            var command1 = dbContext.CreateCommand();

            command1.CommandText = @"
            ALTER TABLE AddonDates
            ADD CONSTRAINT FK_AddonDates_ProductAddons
            FOREIGN KEY (ProductAddon_ID)
            REFERENCES ProductAddons(ID)";
            command1.ExecuteNonQuery();

        }
    }
}
