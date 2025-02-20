using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations;

public class CreateUsersTable: IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<User>(tableOptions: TableOptions.CheckExistence);
    }
}