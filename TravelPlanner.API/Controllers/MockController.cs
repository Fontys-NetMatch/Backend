using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.API.Response.Success.Product;
using TravelPlanner.Domain.Interfaces.BLL.MockGeneration;
using TravelPlanner.Domain.New_Models.Entities.Product;

namespace TravelPlanner.API.Controllers
{
    public class MockController : Controller
    {
        public IProductInformationFactory Factory;

        public MockController(IProductInformationFactory factory)
        {
            this.Factory = factory;
        }

        public static void Mock(WebApplication app)
        {
            app.MapGet("/GenerateMock/{amount:int}", (
                    [FromRoute] int amount,
                    [FromServices] MockController controller
                ) => controller.GenerateMock(amount))
                .WithName("GenerateMock")
                .WithDescription("Generate mock product(s) in the current database")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("product")
                .WithOrder(4)
                .WithOpenApi();
        }


        private SuccessResponse GenerateMock(int ammount)
        {
            try
            {
                for (int i = 0; i < ammount; i++)
                {
                    ProductInformation product = Factory.GenerateRandom();
                    // to database
                    
                }
                var response = new SuccessResponse("Product added successfully");
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            
            return null;
        }
    }
}