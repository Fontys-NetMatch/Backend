using System;
using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Entities.Media;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("DiscountInformations")]
    public class DiscountInformation
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column, Nullable]
        public int DiscountTypeID { get; set; }

        [Association(ThisKey = nameof(DiscountTypeID), OtherKey = nameof(DiscountTypeEntity.Id), CanBeNull = false)]
        public DiscountTypeEntity DiscountTypeEntity { get; set; } = null!;


        [Column(DataType = LinqToDB.DataType.VarChar, Length = 255), Nullable]
        public string? Title { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Content { get; set; }

        [Column(DataType = LinqToDB.DataType.DateTime), Nullable]
        public DateTime? StartDate { get; set; }

        [Column(DataType = LinqToDB.DataType.DateTime), Nullable]
        public DateTime? EndDate { get; set; }
    }
}
