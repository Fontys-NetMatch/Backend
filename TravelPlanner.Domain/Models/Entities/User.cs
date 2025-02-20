using LinqToDB;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities;

[Table("Users")]
public record User
{

    [Column, PrimaryKey, Identity]
    public int Id { get; set; }

    [Column(Length = 255), NotNull]
    public required string Firstname { get; set; }

    [Column(Length = 255), NotNull]
    public required string Lastname { get; set; }

    [Column(Length = 255), NotNull]
    public required string Email { get; set; }

    [Column(Length = 255), Nullable]
    public string? Phone { get; set; }

    [Column(DataType = DataType.Binary), NotNull]
    public required string Password { get; set; }

}