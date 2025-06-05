/*using System.Collections.Generic;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Facility;

public class FacilityInformation
{


    public string? Id { get; set; }
    public FacilityType FacilityType { get; set; }
    public string? Value { get; set; }
    public List<string>? Values { get; set; }
    
    public FacilityInformation(string? id = null, FacilityType facilityType = default, string? value = null, List<string>? values = null)
    {
        Id = id;
        FacilityType = facilityType;
        Value = value;
        Values = values;
    }
}*/


/*using LinqToDB.Mapping;
using LinqToDB.Data;

namespace TravelPlanner.Domain.New_Models.Entities.Facility;

[Table("FacilityInformations")]
public class FacilityInformation
{
    [PrimaryKey]
    [Column(DataType = LinqToDB.DataType.VarChar, Length = 191), NotNull]
    public string Id { get; set; }

    [Column, NotNull]
    public int FacilityTypeId { get; set; }

    [Column(DataType = LinqToDB.DataType.Text), Nullable]
    public string? Value { get; set; }

    [Column, Nullable]
    public string? ParentFacilityId { get; set; }
}*/

using LinqToDB.Mapping;
using LinqToDB.Data;

namespace TravelPlanner.Domain.New_Models.Entities.Facility
{
    [Table("FacilityInformations")]
    public class FacilityInformation
    {
        [PrimaryKey]
        [Column(DataType = LinqToDB.DataType.VarChar, Length = 191), NotNull]
        public string Id { get; set; } = null!;

        [Column, NotNull]
        public int FacilityTypeId { get; set; }

        [Column(DataType = LinqToDB.DataType.Text), Nullable]
        public string? Value { get; set; }

        [Column(DataType = LinqToDB.DataType.VarChar, Length = 191), Nullable]
        public string? ParentFacilityId { get; set; }

    }
}

