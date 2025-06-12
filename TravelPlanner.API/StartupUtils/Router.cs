using TravelPlanner.API.Controllers;

namespace TravelPlanner.API.StartupUtils;

public static class Router
{

    public static void Register(WebApplication app)
    {
        StatusController.Register(app);
        AuthController.Register(app);

        ProductController.Register(app);
        ProductTranslationController.Register(app);


        QuotationController.Register(app);
        CustomerController.Register(app);
       
    }

}