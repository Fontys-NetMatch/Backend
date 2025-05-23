using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Success.Customer;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;

namespace TravelPlanner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerContainer _container;

        public CustomerController(ICustomerContainer container)
        {
            _container = container;
        }

        public static void Register(WebApplication app)
        {
            app.MapGet("/customer/{id:int}", (
                    [FromRoute] int id,
                    [FromServices] CustomerController controller
                ) => controller.GetCustomerById(id))
                .WithName("GetCustomerById")
                .WithDescription("Get a customer by Id")
                .Produces<Customer>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Customer")
                .WithOpenApi();

            app.MapGet("/customer", (
                    [FromServices] CustomerController controller
                ) => controller.GetAllCustomers())
                .WithName("GetAllCustomers")
                .WithDescription("Get all customers")
                .Produces<List<Customer>>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Customer")
                .WithOpenApi();

            app.MapPost("/customer", (
                [FromBody] Customer customer,
                [FromServices] CustomerController controller
            ) => controller.CreateCustomer(customer))
                .WithName("CreateCustomer")
                .WithDescription("Create a new customer")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Customer")
                .WithOpenApi();

            app.MapPut("/customer/{id:int}", (
                [FromRoute] int id,
                [FromBody] Customer customer,
                [FromServices] CustomerController controller
            ) => controller.UpdateCustomer(id, customer))
                .WithName("UpdateCustomer")
                .WithDescription("Update an existing customer")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Customer")
                .WithOpenApi();
        }

        private BaseResponse GetCustomerById(int id)
        {
            try
            {
                var customer = _container.GetByIdAsync(id).Result;
                return customer == null
                    ? new ErrorResponse("Customer not found")
                    : new CustomerResponse(customer);
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }


        private BaseResponse GetAllCustomers()
        {
            try
            {
                var customers = _container.GetAllAsync().Result;
                return new CustomersResponse(customers);
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse CreateCustomer(Customer customer)
        {
            try
            {
                _container.CreateAsync(customer);
                return new SuccessResponse("Customer created successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }

        private BaseResponse UpdateCustomer(int id, Customer customer)
        {
            try
            {
                customer.Id = id;
                _container.UpdateAsync(customer).Wait();
                return new SuccessResponse("Customer updated successfully");
            }
            catch (Exception e)
            {
                return new ErrorResponse(e.Message);
            }
        }
    }
}
