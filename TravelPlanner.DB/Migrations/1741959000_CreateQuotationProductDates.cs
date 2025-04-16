using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations;

public class CreateQuotationProductDates : IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<QuotationProductDate>(tableOptions: TableOptions.CheckExistence);

        DbUtils.GenerateForeignKey(
            dbContext,
            "QuotationProductDates",
            "ProductDates",
            "ID"
        );

        DbUtils.GenerateForeignKey(
            dbContext,
            "QuotationProductDates",
            "Quotations",
            "ID"
        );

    }
}