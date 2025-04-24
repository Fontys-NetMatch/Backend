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

            // Download Quotation as PDF
            app.MapGet("/quotations/{id}/pdf", async (
    int id,
    HttpContext context,
    [FromServices] QuotationController controller
) =>
            {
                var fileContentResult = await controller.GetQuotationPdf(id); // This now returns FileContentResult
                return Results.File(fileContentResult.FileContents, fileContentResult.ContentType, fileContentResult.FileDownloadName);
            })
.WithName("DownloadQuotationPdf")
.WithDescription("Download the quotation as a PDF")
.Produces(StatusCodes.Status200OK, contentType: "application/pdf")
.Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
.RequiresJwtToken()
.WithTags("Quotation")
.WithOpenApi();
        }

        private async Task<FileContentResult> GetQuotationPdf(int id)
        {
            return await _container.GeneratePdfAsync(id);
            throw new NotImplementedException();
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
                Quotation? quotation = _container.GetQuotationById(id).Result;
                if (quotation == null)
                {
                    return new NoContentResponse();
                }

                var response = new QuotationResponse(
                    id: quotation.ID,
                    name: quotation.Name,
                    status: quotation.Status,
                    customerId: quotation.Customer_ID
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
                var quotations = _container.GetAllActiveQuotations().Result;
                if (quotations.Count == 0)
                {
                    return new NoContentResponse();
                }

                // Convert each Quotation to a QuotationResponse
                var quotationResponses = quotations.Select(quotation => new QuotationResponse(
                    id: quotation.ID,
                    name: quotation.Name,
                    status: quotation.Status,
                    customerId: quotation.Customer_ID
                )).ToList();

                return new QuotationsResponse(quotationResponses);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }
    }
}
