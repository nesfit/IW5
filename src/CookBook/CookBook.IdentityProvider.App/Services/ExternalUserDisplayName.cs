using System.Security.Claims;

namespace CookBook.IdentityProvider.App.Services;

internal static class ExternalUserDisplayName
{
    public const string StoredClaimType = "cookbook:external_display_name";

    public static string GetDisplayName(ClaimsPrincipal principal, string fallback = "signed-in user")
    {
        // These claims are display labels only, never account-linking keys.
        var fullName = FindFirst(principal, "name");
        if (fullName is not null)
        {
            return fullName;
        }

        var personName = string.Join(" ", new[]
        {
            FindFirst(principal, "given_name", ClaimTypes.GivenName),
            FindFirst(principal, "family_name", ClaimTypes.Surname)
        }.Where(part => part is not null));
        if (!string.IsNullOrWhiteSpace(personName))
        {
            return personName;
        }

        return FindFirst(principal, ClaimTypes.Name, "preferred_username", "email", ClaimTypes.Email)
            ?? fallback;
    }

    private static string? FindFirst(ClaimsPrincipal principal, params string[] claimTypes)
        => claimTypes
            .SelectMany(principal.FindAll)
            .Select(claim => claim.Value.Trim())
            .FirstOrDefault(value => value.Length > 0);
}
