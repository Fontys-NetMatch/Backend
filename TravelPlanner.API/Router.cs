using TravelPlanner.API.Controllers;

namespace TravelPlanner.API;

public class Router
{

    public Router(WebApplication app)
    {
        StatusController.Register(app);
    }

}