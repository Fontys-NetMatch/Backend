namespace TravelPlanner.API.Controllers;

public abstract class StatusController
{

    public static void Register(WebApplication app)
    {
        app.MapGet("/status", Status)
            .WithName("Get Api Status")
            .WithDescription("Get the current status of the API")
            .WithOpenApi();
    }

    private static object Status(HttpContext? context)
    {
        return new
        {
            Status = "API Running"
        };
    }

}