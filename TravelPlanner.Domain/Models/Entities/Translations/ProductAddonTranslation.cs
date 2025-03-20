using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Domain.Models.Entities.Translations
{
    [Table("ProductAddonTranslation")]
    public class ProductAddonTranslation
    {
        [Column, PrimaryKey, Identity]
        public int ID { get; set; }

        [Column(Length = 100), NotNull]
        public int Name { get; set; }

        [Column, NotNull]
        public string Description { get; set; }

        [Column(Length = 10), NotNull]
        public string LangIsoCode { get; set; }
    }
}
