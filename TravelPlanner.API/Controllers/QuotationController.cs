using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response.Success.Quotation;
using TravelPlanner.Domain.Models.Request.Quotation;

namespace TravelPlanner.API.Controllers
{
    public class QuotationController : Controller
    {
        private readonly IQuotationContainer _container;

        public QuotationController(IQuotationContainer container)
        {
            _container = container;
        }

        public static void Register(WebApplication app)
        {
            // Create Quotation
            app.MapPost("/quotation/", (
                HttpContext context,
                [FromBody] QuotationData quotation,
                [FromServices] QuotationController controller
            ) => controller.CreateQuotation(context, quotation))
                .WithName("CreateQuotation")
                .WithDescription("Create a new quotation")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Quotation")
                .WithOpenApi();

            // Get Quotation by ID
            app.MapGet("/quotation/{id}", (
                HttpContext context,
                [FromRoute] int id,
                [FromServices] QuotationController controller
            ) => controller.GetQuotation(context, id))
                .WithName("GetQuotation")
                .WithDescription("Get a quotation by ID")
                .Produces<QuotationResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Quotation")
                .WithOpenApi();

            // Update Quotation
            app.MapPut("/quotation/", (
                HttpContext context,
                [FromBody] QuotationUpdateData quotation,
                [FromServices] QuotationController controller
            ) => controller.UpdateQuotation(context, quotation))
                .WithName("UpdateQuotation")
                .WithDescription("Update an existing quotation")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Quotation")
                .WithOpenApi();

            // Soft Delete Quotation
            app.MapDelete("/quotation/{id}", (
                HttpContext context,
                [FromRoute] int id,
                [FromServices] QuotationController controller
            ) => controller.SoftDeleteQuotation(context, id))
                .WithName("SoftDeleteQuotation")
                .WithDescription("Soft delete a quotation by ID")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Quotation")
                .WithOpenApi();

            // Get all Active Quotations
            app.MapGet("/quotations/active", (
                HttpContext context,
                [FromServices] QuotationController controller
            ) => controller.GetAllActiveQuotations(context))
                .WithName("GetAllActiveQuotations")
                .WithDescription("Get all active quotations")
                .Produces<QuotationsResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Quotation")
                .WithOpenApi();
        }

        private BaseResponse CreateQuotation(HttpContext? context, QuotationData quotation)
        {
            try
            {
                _container.CreateQuotation(quotation);
                return new SuccessResponse("Quotation created successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }

        private BaseResponse GetQuotation(HttpContext? context, int id)
        {
            try
            {
                Quotation? quotation = _container.GetQuotationByIdAsync(id).Result;
                if (quotation == null)
                {
                    return new ErrorResponse("Quotation not found");
                }

                var response = new QuotationResponse(
                    id: quotation.ID,
                    name: quotation.Name,
                    isActive: quotation.IsActive,
                    customerId: quotation.Customer_ID,
                    message: "Quotation retrieved successfully"
                );
                return response;
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }

        private BaseResponse UpdateQuotation(HttpContext? context, QuotationUpdateData quotation)
        {
            try
            {
                _container.UpdateQuotation(quotation).Wait();
                return new SuccessResponse("Quotation updated successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }

        private BaseResponse SoftDeleteQuotation(HttpContext? context, int id)
        {
            try
            {
                _container.SoftDeleteQuotation(id).Wait();
                return new SuccessResponse("Quotation soft-deleted successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }

        private BaseResponse GetAllActiveQuotations(HttpContext? context)
        {
            try
            {
                var quotations = _container.GetAllActiveQuotationsAsync().Result;
                if (quotations == null || !quotations.Any())
                {
                    return new ErrorResponse("No active quotations found");
                }

                // Convert each Quotation to a QuotationResponse
                var quotationResponses = quotations.Select(q => new QuotationResponse(
                    id: q.ID,
                    name: q.Name,
                    isActive: q.IsActive,
                    customerId: q.Customer_ID,
                    message: "Quotation retrieved successfully"
                )).ToList();

                return new QuotationsResponse(quotationResponses, "Active quotations retrieved successfully");
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }
    }
}
