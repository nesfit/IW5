using System.Security.Claims;

namespace CookBook.Web.BL.Extensions;

public static class UserDisplayNameExtensions
{
    public static string GetDisplayName(this ClaimsPrincipal user)
    {
        var fullName = FindFirst(user, "name");
        if (fullName is not null)
        {
            return fullName;
        }

        var personName = string.Join(" ", new[]
        {
            FindFirst(user, "given_name", ClaimTypes.GivenName),
            FindFirst(user, "family_name", ClaimTypes.Surname)
        }.Where(part => part is not null));
        if (!string.IsNullOrWhiteSpace(personName))
        {
            return personName;
        }

        return FindFirst(user, ClaimTypes.Name, "username", "preferred_username", "email", ClaimTypes.Email)
            ?? "signed-in user";
    }

    private static string? FindFirst(ClaimsPrincipal user, params string[] claimTypes)
        => claimTypes
            .SelectMany(user.FindAll)
            .Select(claim => claim.Value.Trim())
            .FirstOrDefault(value => value.Length > 0);
}
