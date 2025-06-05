using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.Domain.New_Models.Entities.Product;
using TravelPlanner.Domain.New_Models.Enums;

namespace TravelPlanner.Domain.New_Models.Entities.Program;

public class DayToDayProgram
{
    public TransportTypeValue TransportType { get; set; }
    public List<DayProgram?> DayPrograms { get; set; }
    public ProductDescription? Descriptions { get; set; }
}