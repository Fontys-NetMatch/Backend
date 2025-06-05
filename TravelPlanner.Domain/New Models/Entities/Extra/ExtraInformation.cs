using System;
using System.Collections.Generic;
using LinqToDB.Mapping;
using TravelPlanner.Domain.New_Models.Entities.Media;

namespace TravelPlanner.Domain.Models.Entities
{
    [Table("ExtraInformations")]
    public class ExtraInformation
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
        public string? Code { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 255), Nullable]
        public string? Name { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 255), Nullable]
        public string? ContentTitle { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Description { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 100), Nullable]
        public string? Category { get; set; }
        public List<MediaContext>? Images { get; set; }
        public List<ExtraOption>? Options { get; set; }
    }
}
