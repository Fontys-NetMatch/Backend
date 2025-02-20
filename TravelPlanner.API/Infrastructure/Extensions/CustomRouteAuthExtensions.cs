using Microsoft.AspNetCore.Authorization;
using TravelPlanner.API.Infrastructure.Extensions.AuthAttributes;

namespace TravelPlanner.API.Infrastructure.Extensions;

public static class CustomRouteAuthExtensions
{
    private static readonly IAllowAnonymous _requiresJtwToken = new RequiresTokenAttribute();
    private static readonly IAllowAnonymous _optionalJtwToken = new OptionalTokenAttribute();

    public static TBuilder RequiresJwtToken<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder => { endpointBuilder.Metadata.Add(_requiresJtwToken); });
        return builder;
    }

    public static TBuilder OptionalJwtToken<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder => { endpointBuilder.Metadata.Add(_optionalJtwToken); });
        return builder;
    }
}