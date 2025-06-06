using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.New_Models.Entities.Product;
using TravelPlanner.Domain.New_Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreatHotelLocationAndContactInformationsTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<HotelLocationAndContactInformation>(tableOptions: TableOptions.CheckExistence);

            DbUtils.GenerateForeignKey(
                dbContext,
                "HotelLocationAndContactInformations",
                "GeoCoordinateId",
                "GeoCoordinates",
                "Id"
            );
        }
    }
}
