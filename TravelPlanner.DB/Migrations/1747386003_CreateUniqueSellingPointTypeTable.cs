using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Enums;
using TravelPlanner.Domain.Models.Entities.UniqueSellingPoints;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations
{
    public class CreateUniqueSellingPointTypeTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<UniqueSellingPointTypeEntity>(tableOptions: TableOptions.CheckExistence);

            var types = Enum.GetValues(typeof(UniqueSellingPointType))
                            .Cast<UniqueSellingPointType>()
                            .Select(e => new UniqueSellingPointTypeEntity
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
