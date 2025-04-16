using TravelPlanner.API.Controllers;

namespace TravelPlanner.API;

public class Router
{

    public Router(WebApplication app)
    {
        StatusController.Register(app);
        AuthController.Register(app);

        ProductController.Register(app);
        ProductTranslationController.Register(app);

        ProductTypeController.Register(app);

        QuotationController.Register(app);
        CustomerController.Register(app);
       
    }

}