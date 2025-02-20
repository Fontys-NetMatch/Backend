namespace TravelPlanner.API.Infrastructure.Middleware;

public class EndpointMetadataLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public EndpointMetadataLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Get the endpoint associated with the current request
        var endpoint = context.GetEndpoint();

        if (endpoint != null)
        {
            Console.WriteLine($"Endpoint: {endpoint.DisplayName}");

            // Access each metadata item associated with the endpoint
            foreach (var metadata in endpoint.Metadata)
            {
                Console.WriteLine($"Metadata Type: {metadata.GetType().Name}");
                Console.WriteLine(metadata.ToString());
            }
        }
        else
        {
            Console.WriteLine("No endpoint metadata available for this request.");
        }

        // Call the next middleware in the pipeline
        await _next(context);
    }
}