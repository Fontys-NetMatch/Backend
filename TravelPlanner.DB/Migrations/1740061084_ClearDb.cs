using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations;

[ForceOnDev]
public class ClearDb: IMigration
{

    public void Up(DbContext dbContext)
    {
        var query = new StringBuilder();

        query.AppendLine("SET FOREIGN_KEY_CHECKS = 0;");
        query.AppendLine("DROP TABLE IF EXISTS Quotations;");
        query.AppendLine("DROP TABLE IF EXISTS Customers;");
        query.AppendLine("DROP TABLE IF EXISTS Products;");
        query.AppendLine("DROP TABLE IF EXISTS Users;");
        query.AppendLine("DROP TABLE IF EXISTS ProductTranslations;");
        query.AppendLine("DROP TABLE IF EXISTS ProductImages;");
        query.AppendLine("DROP TABLE IF EXISTS ProductTypes;");
        query.AppendLine("DROP TABLE IF EXISTS ProductTypeTranslations;");
        query.AppendLine("DROP TABLE IF EXISTS ProductDates;");
        query.AppendLine("DROP TABLE IF EXISTS ProductAddons;");        
        query.AppendLine("DROP TABLE IF EXISTS AddonDates;");
        query.AppendLine("DROP TABLE IF EXISTS ProductAddonTranslations;");
        query.AppendLine("DELETE FROM Migrations WHERE ClassName != 'ClearDb';");
        query.AppendLine("SET FOREIGN_KEY_CHECKS = 1;");

        var command = dbContext.CreateCommand();
        command.CommandText = query.ToString();
        command.ExecuteNonQuery();

    }

}