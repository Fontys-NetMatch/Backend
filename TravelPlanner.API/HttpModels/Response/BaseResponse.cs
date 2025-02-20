using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TravelPlanner.API.Models.Response;

public record BaseResponse
{
    public bool Success => StatusCode is >= 200 and < 300;
    public string Message { get; init; } = "Unknown error";

    // ReSharper disable once MemberCanBeProtected.Global
    public int StatusCode { get; init; } = 200;
    public string StatusDescription => GetStatusDescription();
    public Dictionary<string, object?> Data { get; init; } = new();

    protected BaseResponse(string message)
    {
        Message = message;
    }

    public IResult GetResults()
    {
        var json = JsonSerializer.Serialize(this);
        if (Data.Count == 0) json = json.Replace(""","Data":{}""", "");

        return TypedResults.Content(
            json,
            "application/json",
            Encoding.Default,
            StatusCode
        );
    }

    public IActionResult GetActionResult()
    {
        return new ContentResult
        {
            Content = JsonSerializer.Serialize(this),
            ContentType = "application/json",
            StatusCode = StatusCode
        };
    }

    public Task<IResult> GetResultsAsync()
    {
        return Task.FromResult(GetResults());
    }

    public Task<IActionResult> GetActionResultAsync()
    {
        return Task.FromResult(GetActionResult());
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    private string GetStatusDescription()
    {
        return StatusCode switch
        {
            100 => "Continue",
            101 => "Switching Protocols",
            102 => "Processing",
            103 => "Early Hints",
            200 => "OK",
            201 => "Created",
            202 => "Accepted",
            203 => "Non-Authoritative Information",
            204 => "No Content",
            205 => "Reset Content",
            206 => "Partial Content",
            207 => "Multi-Status",
            208 => "Already Reported",
            226 => "IM Used",
            400 => "Bad Request",
            401 => "Unauthorized",
            402 => "Payment Required",
            403 => "Forbidden",
            404 => "Not Found",
            405 => "Method Not Allowed",
            406 => "Not Acceptable",
            407 => "Proxy Authentication Required",
            408 => "Request Timeout",
            409 => "Conflict",
            410 => "Gone",
            411 => "Length Required",
            412 => "Precondition Failed",
            413 => "Payload Too Large",
            414 => "URI Too Long",
            415 => "Unsupported Media Type",
            416 => "Range Not Satisfiable",
            417 => "Expectation Failed",
            418 => "I'm a teapot",
            421 => "Misdirected Request",
            422 => "Unprocessable Entity",
            423 => "Locked",
            424 => "Failed Dependency",
            425 => "Too Early",
            426 => "Upgrade Required",
            428 => "Precondition Required",
            429 => "Too Many Requests",
            431 => "Request Header Fields Too Large",
            451 => "Unavailable For Legal Reasons",
            500 => "Internal Server Error",
            501 => "Not Implemented",
            502 => "Bad Gateway",
            503 => "Service Unavailable",
            504 => "Gateway Timeout",
            505 => "HTTP Version Not Supported",
            506 => "Variant Also Negotiates",
            507 => "Insufficient Storage",
            508 => "Loop Detected",
            510 => "Not Extended",
            511 => "Network Authentication Required",
            _ => "Unknown"
        };
    }
}