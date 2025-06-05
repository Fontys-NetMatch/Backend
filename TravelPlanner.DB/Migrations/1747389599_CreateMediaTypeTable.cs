using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations
{
    public class CreateMediaTypeTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<MediaTypeEntity>(tableOptions: TableOptions.CheckExistence);

            var types = Enum.GetValues(typeof(MediaType))
                                .Cast<MediaType>()
                                .Select(e => new MediaTypeEntity
                                {
                                    Id = (int)e,
                                    Name = e.ToString()
                                });

            foreach (var type in types)
            {
                dbContext.Insert(type);
            }
        }
    }
}