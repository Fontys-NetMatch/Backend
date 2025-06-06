using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Product
{
    [Table("ProductCodes")]
    public class ProductCodeEntity
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, NotNull]
        public TransportTypeValue TransportType { get; set; }

        [Column, Nullable]
        public string? Code { get; set; }
    }
}
