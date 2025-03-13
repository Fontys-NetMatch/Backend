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

        var command = dbContext.CreateCommand();
        // create foreign key between Quotations CustomerId and Customers Id
        command.CommandText = @"
            ALTER TABLE Quotations
            ADD CONSTRAINT FK_Quotations_Customers
            FOREIGN KEY (CustomerId)
            REFERENCES Customers(Id)";
        command.ExecuteNonQuery();

    }
}