using TravelPlanner.API.Models.Response.Error;

namespace TravelPlanner.API.Infrastructure.Middleware;

public class AuthErrorMiddleware
{
    private readonly RequestDelegate _next;

    public AuthErrorMiddleware(
        RequestDelegate next
    )
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Items["AuthError"] is string authError)
        {
            UnauthorizedResponse response = new(authError);
            context.Response.StatusCode = response.StatusCode;
            await context.Response.WriteAsJsonAsync(response);
            return;
        }

        await _next(context);
    }
}