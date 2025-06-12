using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Domain.New_Models.Enums.EnumsEntity
{
    [Table("FacilityTypes")]
    public class FacilityTypeEntity
    {
        [Column, PrimaryKey]
        public int Id { get; set; }

        [Column, NotNull]
        public string Name { get; set; } = null!;
    }
}
