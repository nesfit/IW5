using System.Security.Claims;

namespace CookBook.Web.BL.Extensions;

public static class UserDisplayNameExtensions
{
    public static string GetDisplayName(this ClaimsPrincipal user)
        => new[] { "username", "preferred_username", "name", ClaimTypes.Name, "email", ClaimTypes.Email }
            .SelectMany(user.FindAll)
            .Select(claim => claim.Value)
            .FirstOrDefault(value => !string.IsNullOrWhiteSpace(value))
            ?? "signed-in user";
}
