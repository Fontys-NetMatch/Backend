using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations
{
    public class DiscountTypes : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<DiscountTypeEntity>(tableOptions: TableOptions.CheckExistence);

            var types = Enum.GetValues(typeof(DiscountType))
                                .Cast<DiscountType>()
                                .Select(e => new DiscountTypeEntity
                                {
                                    Id = (int)e,
                                    Name = e.ToString()
                                });

            foreach (var type in types)
            {
                dbContext.InsertOrReplace(type);
            }
        }
    }
}