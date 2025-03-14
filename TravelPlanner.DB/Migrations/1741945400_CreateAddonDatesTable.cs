using LinqToDB;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Models.Entities;

public class CreateAddonDates : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<AddonDate>(tableOptions: TableOptions.CheckExistence);

        var command1 = dbContext.CreateCommand();
        command1.CommandText = @"
            ALTER TABLE AddonDates
            ADD CONSTRAINT FK_AddonDates_ProductDates
            FOREIGN KEY (ProductDate_ID)
            REFERENCES ProductDates(ID)";
        command1.ExecuteNonQuery(); 

        var command2 = dbContext.CreateCommand();
        command2.CommandText = @"
            ALTER TABLE AddonDates
            ADD CONSTRAINT FK_AddonDates_ProductAddons
            FOREIGN KEY (ProductAddon_ID)
            REFERENCES ProductAddons(ID)";
        command2.ExecuteNonQuery(); 
    }
}
