using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities;

[Table("DiscountInformations")]
public class DiscountInformation
{
    [PrimaryKey, Identity]
    public int Id { get; set; }

    [Column, Nullable]
    public int DiscountTypeID { get; set; }

    [Association(ThisKey = nameof(DiscountTypeID), OtherKey = nameof(DiscountTypeEntity.Value), CanBeNull = false)]
    public DiscountType? DiscountTypeEntity { get; set; } = null!;

    [Column(DataType = LinqToDB.DataType.VarChar, Length = 255), Nullable]
    public string? Title { get; set; }

    [Column(DataType = LinqToDB.DataType.Text), Nullable]
    public string? Content { get; set; }

    [Column(DataType = LinqToDB.DataType.DateTime), Nullable]
    public DateTime? StartDate { get; set; }

    [Column(DataType = LinqToDB.DataType.DateTime), Nullable]
    public DateTime? EndDate { get; set; }

    public DiscountInformation(
        int discountTypeID,
        DiscountType discountType,
        string? title,
        string? content,
        DateTime? startDate,
        DateTime? endDate)
    {
        DiscountTypeID = discountTypeID;
        DiscountTypeEntity = discountType;
        Title = title;
        Content = content;
        StartDate = startDate;
        EndDate = endDate;
    }
}