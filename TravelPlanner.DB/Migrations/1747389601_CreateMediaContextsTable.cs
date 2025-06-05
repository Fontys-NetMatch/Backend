using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.DB.Lib.MigrationsManager;
using TravelPlanner.DB.Lib;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.DB.Migrations
{
    public class CreateMediaTables : IMigration
    {
        public void Up(DbContext dbContext)
        {
            dbContext.CreateTable<MediaContext>(tableOptions: TableOptions.CheckExistence);

            DbUtils.GenerateForeignKey(
                dbContext,
                "MediaContexts",
                "MediaTypeId",
                "MediaTypes",
                "Id"
                );

            DbUtils.GenerateForeignKey(
                dbContext,
                "MediaContexts",
                "MediaSourceId",
                "MediaSources",
                "Id"
                );
        }
    }

}
