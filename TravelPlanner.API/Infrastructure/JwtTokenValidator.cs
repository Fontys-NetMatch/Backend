using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.IdentityModel.Tokens;
using TravelPlanner.API.Infrastructure.Extensions.AuthAttributes;
using TravelPlanner.Domain.Interfaces;

namespace TravelPlanner.API.Infrastructure;

public class JwtTokenValidator
{

    private IAppConfig _config;

    public JwtTokenValidator(IAppConfig config)
    {
        _config = config;
    }

    public Task VerifyToken(MessageReceivedContext context)
    {
        var metadata = context.HttpContext.GetEndpoint()?.Metadata;
        if (metadata is null) return Task.CompletedTask;

        var requiresJwtToken = metadata.GetMetadata<RequiresTokenAttribute>() != null;
        var optionalJwtToken = metadata.GetMetadata<OptionalTokenAttribute>() != null;

        // If the endpoint does not require a token, return
        if (!requiresJwtToken && !optionalJwtToken) return Task.CompletedTask;

        string path = context.HttpContext.Request.Path;
        if (path.StartsWith("/swagger")) return Task.CompletedTask;

        string? authHeader = context.Request.Headers.Authorization;
        if (requiresJwtToken && authHeader is null)
        {
            context.HttpContext.Items.Add("AuthError", "No JWT Token provided");
            return Task.CompletedTask;
        }

        if (requiresJwtToken)
        {
            try
            {
                var jwtToken = authHeader!.Replace("Bearer ", "");

                var jwtTokenHandler = new JwtSecurityTokenHandler();
                // Validation 1 - Validation JWT token format
                var tokenInVerification = jwtTokenHandler.ValidateToken(jwtToken, GetValidationParameters(), out var validatedToken);

                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                    {
                        context.HttpContext.Items.Add("AuthError", "Invalid token");
                        return Task.CompletedTask;
                    }
                }

                // token is valid
                context.HttpContext.Items.Add("JwtToken", jwtToken);
                return Task.CompletedTask;
            }catch (Exception e)
            {
                context.HttpContext.Items.Add("AuthError", "Invalid token");
                return Task.CompletedTask;
            }
        }

        return Task.CompletedTask;
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidIssuer = _config.GetJwtConfig().Issuer,
            ValidAudience = _config.GetJwtConfig().Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetJwtConfig().Secret)) // The same key as the one that generate the token
        };
    }

}