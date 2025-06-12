using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.New_Models.Enums.EnumsEntity;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.DB.Migrations
{
    public class CreateFacilityTypesTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<FacilityTypeEntity>(tableOptions: TableOptions.CheckExistence);

            var types = Enum.GetValues(typeof(FacilityType))
                            .Cast<FacilityType>()
                            .Select(e => new FacilityTypeEntity
                            {
                                Id = (int)e,
                                Name = e.ToString()
                            });

            foreach (var t in types)
            {
                dbContext.InsertOrReplace(t);
            }
        }
    }
}
