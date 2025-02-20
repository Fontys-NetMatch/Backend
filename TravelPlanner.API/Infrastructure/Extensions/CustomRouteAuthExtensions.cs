using Microsoft.AspNetCore.Authorization;
using TravelPlanner.API.Infrastructure.Extensions.AuthAttributes;

namespace TravelPlanner.API.Infrastructure.Extensions;

public static class CustomRouteAuthExtensions
{
    private static readonly IAllowAnonymous _requiresToken = new RequiresTokenAttribute();
    private static readonly IAllowAnonymous _optionalToken = new OptionalTokenAttribute();

    public static TBuilder RequiresToken<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder => { endpointBuilder.Metadata.Add(_requiresToken); });
        return builder;
    }

    public static TBuilder OptionalToken<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder => { endpointBuilder.Metadata.Add(_optionalToken); });
        return builder;
    }
}