using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("RoomTypes")]
    public class RoomTypeEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        [Column, NotNull]
        public string Name { get; set; } = null!;

    }
}
