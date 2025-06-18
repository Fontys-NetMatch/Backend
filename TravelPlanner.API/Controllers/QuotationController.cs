using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Response;
using TravelPlanner.API.Response.Error;
using TravelPlanner.API.Response.Success;
using TravelPlanner.Domain.Interfaces.BLL;
using TravelPlanner.Domain.Models.Entities;
using TravelPlanner.API.Infrastructure.Extensions;
using TravelPlanner.API.Response.Success.Quotation;
using TravelPlanner.Domain.Interfaces.BLL.Container;
using TravelPlanner.Domain.Interfaces.BLL.Service;
using TravelPlanner.Domain.Models.Request.Quotation;

namespace TravelPlanner.API.Controllers
{
    public class QuotationController : Controller
    {
        private readonly IQuotationContainer _container;
        private readonly IQuotationService _service;
        
        public QuotationController(IQuotationContainer container, IQuotationService service)
        {
            _container = container;
            _service = service;
        }

        public static void Register(WebApplication app)
        {
            // Create Quotation
            app.MapPost("/quotation", (
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

            //// Get all Active Quotations
            //app.MapGet("/quotations/active", (
            //    HttpContext context,
            //    [FromServices] QuotationController controller
            //) => controller.GetAllActiveQuotations(context))
            //    .WithName("GetAllActiveQuotations")
            //    .WithDescription("Get all active quotations")
            //    .Produces<QuotationsResponse>()
            //    .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
            //    .RequiresJwtToken()
            //    .WithTags("Quotation")
            //    .WithOpenApi();

            // Get flat commision quotation price
            app.MapPost("/quotations/{id}/flatcommision", (
                    HttpContext context,
                    [FromRoute] int id,
                    [FromBody] double commision,
                    [FromServices] QuotationController controller
                ) => controller.GetQuotationValueFlat(context,id , commision))
                .WithName("GetFlatCommision")
                .WithDescription("Get the quotation price with a flat commision")
                .Produces<PriceCalcResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Price")
                .WithOpenApi();
            
            // Get percentile commision quotation price
            app.MapPost("/quotations/{id}/percentilecommision", (
                    HttpContext context,
                    [FromRoute] int id,
                    [FromBody] double percentile,
                    [FromServices] QuotationController controller
                ) => controller.GetQuotationValueFlat(context,id , percentile))
                .WithName("GetPercentileCommision")
                .WithDescription("Get the quotation price with a percentile commision")
                .Produces<PriceCalcResponse>()
                .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Price")
                .WithOpenApi();

            // Download Quotation as PDF
            app.MapGet("/quotations/{id}/pdf", (
                    int id, HttpContext context, [FromServices] QuotationController controller) =>
                {
                    var response = controller.GetQuotationPdf(id); // Now returns QuotationPdfResponse
                    if (response.Base64Pdf is not null && response.FileName is not null && response.ContentType is not null)
                    {
                        var fileBytes = Convert.FromBase64String(response.Base64Pdf);
                        return Results.File(fileBytes, response.ContentType, response.FileName);
                    }
                    
                    return response.GetResults();
                })
                .WithName("DownloadQuotationPdf")
                .WithDescription("Download the quotation as a PDF")
                .Produces(StatusCodes.Status200OK, contentType: "application/pdf")
                .Produces<QuotationPdfResponse>(StatusCodes.Status400BadRequest)
                .Produces<QuotationPdfResponse>(StatusCodes.Status404NotFound)
                .Produces<QuotationPdfResponse>(StatusCodes.Status500InternalServerError)
                .RequiresJwtToken()
                .WithTags("Quotation")
                .WithOpenApi();
        }

        private QuotationPdfResponse  GetQuotationPdf(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return QuotationPdfResponse.ErrorResponse(400, "Invalid Quotation ID. It must be a positive integer.");
                }
                var pdfResult = _container.GeneratePdfAsync(id).GetAwaiter().GetResult();
                if (pdfResult == null)
                {
                    return QuotationPdfResponse.ErrorResponse(404, $"Quotation with ID {id} not found.");
                }

                if (pdfResult is FileContentResult fileContentResult)
                {
                    return QuotationPdfResponse.SuccessResponse(
                        fileContentResult.FileContents,
                        fileContentResult.ContentType,
                        fileContentResult.FileDownloadName
                    );
                }
                return QuotationPdfResponse.ErrorResponse(500, "Unexpected result type.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error generating PDF for Quotation ID {id}: {ex.Message}");
                return QuotationPdfResponse.ErrorResponse(500, "An error occurred while generating the PDF.");
            }
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

        //private BaseResponse GetAllActiveQuotations(HttpContext? context)
        //{
        //    try
        //    {
        //        var quotations = _container.GetAllActiveQuotations().Result;
        //        if (quotations.Count == 0)
        //        {
        //            return new NoContentResponse();
        //        }

        //        // Convert each Quotation to a QuotationResponse
        //        var quotationResponses = quotations.Select(quotation => new QuotationResponse(
        //            id: quotation.Id,
        //            name: quotation.Name,
        //            status: quotation.Status,
        //            customerId: quotation.CustomerId
        //        )).ToList();

        //        return new QuotationsResponse(quotationResponses);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ErrorResponse(ex.Message);
        //    }
        //}
        private BaseResponse GetQuotationValueFlat(HttpContext? context, int Id, double Commision)
        {
            try
            {
                Double Price =  _service.FlatCommision(Id, Commision).Result;

                return new PriceCalcResponse(Price);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }
        
        private BaseResponse GetQuotationValuePercentile(HttpContext? context, int Id, double Percentile)
        {
            try
            {
                Double Price =  _service.PercentileCommision(Id, Percentile).Result;

                return new PriceCalcResponse(Price);
            }
            catch (Exception ex)
            {
                return new ErrorResponse(ex.Message);
            }
        }
    }
}
