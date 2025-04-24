using Microsoft.AspNetCore.Authentication.BearerToken;
using TravelPlanner.API.Infrastructure;
using TravelPlanner.Domain.Models;

namespace TravelPlanner.API.StartupUtils;

public static class AuthProvider
{

    public static void Register(IServiceCollection services, AppConfig config)
    {
        services.AddAuthorization();
        services.AddAuthentication(option =>
        {
            option.DefaultAuthenticateScheme = BearerTokenDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = BearerTokenDefaults.AuthenticationScheme;
        }).AddBearerToken(options =>
        {
            options.Events = new BearerTokenEvents
            {
                OnMessageReceived = context =>
                {
                    JwtTokenValidator picturaApiKeyValidator = new(config);
                    return picturaApiKeyValidator.VerifyToken(context);
                }
            };
        });
        services.AddCors();
    }

}