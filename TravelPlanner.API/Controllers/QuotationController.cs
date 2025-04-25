using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response.Success.Quotation;
using TravelPlanner.Domain.Models.Request.Quotation;
using TravelPlanner.Domain.Enums;

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

            // Get Quotation by Id
            app.MapGet("/quotation/{id}", (
                HttpContext context,
                [FromRoute] int id,
                [FromServices] QuotationController controller
            ) => controller.GetQuotation(context, id))
                .WithName("GetQuotation")
                .WithDescription("Get a quotation by Id")
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
                .WithDescription("Soft delete a quotation by Id")
                .Produces<SuccessResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Quotation")
                .WithOpenApi();

            // Get all Quotations with optional filters
            app.MapGet("/quotations", (
                [FromQuery] string? name,
                [FromQuery] string? statuses, // e.g. "Draft,Approved"
                [FromQuery] string? searchQuery,
                [FromServices] QuotationController controller
            ) => controller.GetAllQuotations(new QuotationFiltersData
            {
                Name = name,
                Statuses = !string.IsNullOrWhiteSpace(statuses)
                    ? statuses
                        .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                        .Select(s => Enum.TryParse<QuotationStatus>(s, true, out var status) ? status : (QuotationStatus?)null)
                        .Where(s => s.HasValue)
                        .Select(s => s!.Value)
                        .ToList()
                    : null,
                SearchQuery = searchQuery
            }))
            .WithName("GetAllQuotations")
            .WithDescription("Get all quotations with optional filters")
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
                var fileContentResult = await controller.GetQuotationPdf(id);
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
                    id: quotation.Id,
                    name: quotation.Name,
                    status: quotation.Status,
                    customerId: quotation.CustomerId
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

        private BaseResponse GetAllQuotations(QuotationFiltersData filters)
        {
            try
            {
                var quotations = _container.GetAllQuotations(filters).Result;

                if (quotations == null || quotations.Count == 0)
                {
                    return new NoContentResponse();
                }

                var responseItems = quotations.Select(q => new QuotationResponse(
                    id: q.Id,
                    name: q.Name,
                    status: q.Status,
                    customerId: q.CustomerId
                )).ToList();

                return new QuotationsResponse(responseItems);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }
    }
}
