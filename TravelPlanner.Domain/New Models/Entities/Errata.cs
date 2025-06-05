namespace TravelPlanner.Domain.New_Models.Entities;

public class Errata
{


    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    public Errata(string? title = null, string? content = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        Title = title;
        Content = content;
        StartDate = startDate;
        EndDate = endDate;
    }
}