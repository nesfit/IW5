using System.Security.Claims;

namespace CookBook.IdentityProvider.App.Services;

internal static class ExternalUserDisplayName
{
    public const string StoredClaimType = "cookbook:external_display_name";

    public static string GetDisplayName(ClaimsPrincipal principal)
    {
        // These claims are display labels only, never account-linking keys.
        string[] claimTypes = ["preferred_username", "name", ClaimTypes.Name, "email", ClaimTypes.Email];
        foreach (var claimType in claimTypes)
        {
            var value = principal.FindAll(claimType)
                .Select(claim => claim.Value)
                .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value));
            if (value is not null)
            {
                return value;
            }
        }

        return "signed-in user";
    }
}
