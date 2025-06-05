using LinqToDB.Mapping;
using LinqToDB.Data;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Facility
{
    [Table("FacilityInformations")]
    public class FacilityInformation
    {
        [PrimaryKey]
        [Column(DataType = LinqToDB.DataType.VarChar, Length = 191), NotNull]
        public string Id { get; set; } = null!;

        [Column, NotNull]
        public FacilityType FacilityType { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Value { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public List<string>? Values { get; set; }

    }
}

