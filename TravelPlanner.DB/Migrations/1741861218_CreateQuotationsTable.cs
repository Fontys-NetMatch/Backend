using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations;

public class CreateQuotationsTable : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<Quotation>(tableOptions: TableOptions.CheckExistence);

        DbUtils.GenerateForeignKey(
            dbContext,
            "Quotations",
            "Customers",
            "Id"
        );

        DbUtils.GenerateForeignKey(
            dbContext,
            "Quotations",
            "Users",
            "Id"
        );

    }
}