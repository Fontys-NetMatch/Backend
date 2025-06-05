using System;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("Errata")]
    public class Errata
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

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
