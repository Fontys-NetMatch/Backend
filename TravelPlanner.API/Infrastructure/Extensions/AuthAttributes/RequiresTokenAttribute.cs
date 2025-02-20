using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace TravelPlanner.API.Infrastructure.Extensions.AuthAttributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
[DebuggerDisplay("{ToString(),nq}")]
public class RequiresTokenAttribute : Attribute, IAllowAnonymous
{
    /// <inheritdoc />
    /// >
    public override string ToString()
    {
        return "RequiresJwtToken";
    }
}