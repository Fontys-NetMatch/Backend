using System;
using LinqToDB.Mapping;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("Erratas")]
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
        
        public Errata(
            string? title,
            string? content,
            DateTime? startDate,
            DateTime? endDate)
        {
            Title = title;
            Content = content;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
