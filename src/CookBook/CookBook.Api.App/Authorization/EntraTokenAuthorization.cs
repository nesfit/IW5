using System.Security.Claims;

namespace CookBook.Api.App.Authorization;

internal static class EntraTokenAuthorization
{
    public static bool HasApiAccess(ClaimsIdentity identity, string roleClaimType)
    {
        var scopeClaims = identity.FindAll("scp").Concat(identity.FindAll("scope")).ToList();
        var hasUserScope = scopeClaims
            .SelectMany(claim => claim.Value.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Contains("access_as_user", StringComparer.Ordinal);
        var hasAppRole = identity.FindAll(roleClaimType)
            .Any(claim => !string.IsNullOrWhiteSpace(claim.Value));

        return hasUserScope || (scopeClaims.Count == 0 && hasAppRole);
    }
}
