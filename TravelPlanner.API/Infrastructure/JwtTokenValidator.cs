using Microsoft.AspNetCore.Authentication.BearerToken;
using TravelPlanner.API.Infrastructure.Extensions.AuthAttributes;

namespace TravelPlanner.API.Infrastructure;

public class JwtTokenValidator
{

    public Task VerifyToken(MessageReceivedContext context)
    {
        var metadata = context.HttpContext.GetEndpoint()?.Metadata;
        if (metadata is null) return Task.CompletedTask;

        var requiresToken = metadata.GetMetadata<RequiresTokenAttribute>() != null;
        var optionalToken = metadata.GetMetadata<OptionalTokenAttribute>() != null;

        // If the endpoint does not require a token, return
        if (!requiresToken && !optionalToken) return Task.CompletedTask;

        string path = context.HttpContext.Request.Path;
        if (path.StartsWith("/swagger")) return Task.CompletedTask;

        string? authHeader = context.Request.Headers.Authorization;
        if (requiresToken && authHeader is null)
        {
            context.HttpContext.Items.Add("AuthError", "No API key provided");
            return Task.CompletedTask;
        }

        if (requiresToken)
        {
            // var apiKeyCode = authHeader!.Replace("Bearer ", "");
            //
            // // Get the api key by the code
            // var apiKey = _apiKeyBLL.GetByCode(apiKeyCode);
            // if (apiKey == null)
            // {
            //     context.HttpContext.Items.Add("AuthError", "Invalid API key");
            //     return Task.CompletedTask;
            // }
            //
            // // Get the user by the api key
            // var user = _userBLL.GetById(apiKey.UserId);
            // if (user == null)
            // {
            //     context.HttpContext.Items.Add("AuthError", "Invalid API key");
            //     return Task.CompletedTask;
            // }
            //
            // // Pass the user to the next middleware
            // context.HttpContext.Items.Add("ApiKey", apiKey);
            // context.HttpContext.Items.Add("User", user);
        }

        return Task.CompletedTask;
    }
}