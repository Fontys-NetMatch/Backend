using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Domain.Models.Entities.Product
{
    [Table("ProductAddons")]
    public record ProductAddon
    {

        [Column, PrimaryKey, Identity]
        public int ID { get; set; }

        [Column, NotNull]
        public bool IsActive { get; set; }
    }
}