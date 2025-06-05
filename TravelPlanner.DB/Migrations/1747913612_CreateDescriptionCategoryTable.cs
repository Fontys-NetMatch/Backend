using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations
{
    public class DescriptionCategories : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<DescriptionCategoryEntity>(tableOptions: TableOptions.CheckExistence);

            var categories = Enum.GetValues(typeof(DescriptionCategory))
                                .Cast<DescriptionCategory>()
                                .Select(e => new DescriptionCategoryEntity
                                {
                                    Id = (int)e,
                                    Name = e.ToString()
                                });

            foreach (var category in categories)
            {
                dbContext.Insert(category);
            }
        }
    }
}