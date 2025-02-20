using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;

namespace TravelPlanner.API.Controllers;

public class StatusController
{

    public static void Register(WebApplication app)
    {
        app.MapGet("/status", (
                HttpContext context,
                [FromServices] StatusController controller
            ) => controller.Status(context))
            .WithName("Get Api Status")
            .WithDescription("Get the current status of the API")
            .WithOpenApi();
    }

    private object Status(HttpContext? context)
    {
        return new
        {
            Status = "API Running"
        };
    }

}