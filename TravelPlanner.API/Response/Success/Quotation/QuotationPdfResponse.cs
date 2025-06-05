using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelPlanner.API.Response;

public record QuotationPdfResponse : BaseResponse
{
    public string? Base64Pdf { get; init; }
    public string? FileName { get; init; }
    public string? ContentType { get; init; }

    public static QuotationPdfResponse SuccessResponse(byte[] fileBytes, string contentType, string fileName) =>
        new()
        {
            StatusCode = 200,
            Base64Pdf = Convert.ToBase64String(fileBytes),
            ContentType = contentType,
            FileName = fileName
        };

    public static QuotationPdfResponse ErrorResponse(int statusCode, string message) =>
        new()
        {
            StatusCode = statusCode,
            Base64Pdf = null,
            ContentType = null,
            FileName = null
        };
}