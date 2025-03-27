using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;

namespace TravelPlanner.API.Controllers
{
    public class StatusController
    {
        // Method to register the API status route
        public static void Register(WebApplication app)
        {
            // GET method to check the API status
            app.MapGet("/status", (
                HttpContext context,  // Handles the HTTP request
                [FromServices] StatusController controller  // Dependency injection for the controller
            ) => controller.Status(context))  // Calls the method to get the API status
                .WithName("Get Api Status")  // Endpoint name for reference
                .WithDescription("Get the current status of the API")  // Description of the endpoint
                .WithOpenApi();  // Adds to Swagger documentation
        }

        // Method to return the API status
        private object Status(HttpContext? context)
        {
            // Return the API status as a simple object
            return new
            {
                Status = "API Running"  // Message saying the API is running
            };
        }
    }
}
