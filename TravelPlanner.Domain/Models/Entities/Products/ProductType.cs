using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductTypes")]
public record ProductType
{

    [Column, PrimaryKey, Identity]
    public int ID { get; set; }

    [Column, NotNull]
    public bool IsActive { get; set; }

}