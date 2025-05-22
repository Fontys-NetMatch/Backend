using LinqToDB;
using TravelPlanner.DB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Enums;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreateRoomTypeTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<RoomTypeEntity>(tableOptions: TableOptions.CheckExistence);

            var types = Enum.GetValues(typeof(RoomType))
                            .Cast<RoomType>()
                            .Select(e => new RoomTypeEntity
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
