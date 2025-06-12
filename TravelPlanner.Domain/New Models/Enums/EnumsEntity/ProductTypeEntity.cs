using System.ComponentModel;
using LinqToDB;
using LinqToDB.Mapping;
using TravelPlanner.Domain.Models.Entities.Translations;

namespace TravelPlanner.Domain.Models.Entities.Products;

[Table("ProductTypes")]
public record ProductTypeEntity
{

    [Column, PrimaryKey]
    public int Id { get; set; }

    [Column, NotNull]
    public string Name { get; set; } = null!;

}