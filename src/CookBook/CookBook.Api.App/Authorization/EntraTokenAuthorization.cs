using System.Security.Claims;

namespace CookBook.Api.App.Authorization;

internal static class EntraTokenAuthorization
{
    public static bool HasApiAccess(ClaimsIdentity identity, string roleClaimType)
    {
        var hasUserScope = identity.FindFirst("scp")?.Value
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Contains("access_as_user", StringComparer.Ordinal) == true;
        var hasAppRole = identity.FindAll(roleClaimType)
            .Any(claim => !string.IsNullOrWhiteSpace(claim.Value));

        return hasUserScope || hasAppRole;
    }
}
