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
    public class CreateIdentifierTypesTable : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<IdentifierTypeEntity>(tableOptions: TableOptions.CheckExistence);

            var types = Enum.GetValues(typeof(IdentifierType))
                .Cast<IdentifierType>()
                .Select(e => new IdentifierTypeEntity
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
