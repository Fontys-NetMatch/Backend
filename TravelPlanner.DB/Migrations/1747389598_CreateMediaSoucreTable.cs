using LinqToDB;
using TravelPlanner.DB.Lib;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations
{
    public class CreateMediaSourceTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<MediaSourceEntity>(tableOptions: TableOptions.CheckExistence);

            var sources = Enum.GetValues(typeof(MediaSource))
                                .Cast<MediaSource>()
                                .Select(e => new MediaSourceEntity
                                {
                                    Id = (int)e,
                                    Name = e.ToString()
                                });

            foreach (var source in sources)
            {
                dbContext.Insert(source);
            }
        }
    }
}