using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Database.Migrations;

public class CreateUsersTable: IMigration
{
    public void Up(DbContext dbContext)
    {
        dbContext.CreateTable<User>(tableOptions: TableOptions.CheckExistence);
    }
}