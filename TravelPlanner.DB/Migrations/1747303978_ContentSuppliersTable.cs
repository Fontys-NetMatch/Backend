using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations
{
    public class CreateContentSupplierTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<ContentSupplierEntity>(tableOptions: TableOptions.CheckExistence);

            var suppliers = Enum.GetValues(typeof(ContentSupplier))
                                .Cast<ContentSupplier>()
                                .Select(e => new ContentSupplierEntity
                                {
                                    Id = (int)e,
                                    Name = e.ToString()
                                });

            foreach (var supplier in suppliers)
            {
                dbContext.Insert(supplier);
            }

        }
    }
}
