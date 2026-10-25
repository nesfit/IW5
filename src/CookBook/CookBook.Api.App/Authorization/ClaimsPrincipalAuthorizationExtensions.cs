using System.Security.Claims;
using CookBook.Common;

namespace CookBook.Api.App.Authorization;

internal static class ClaimsPrincipalAuthorizationExtensions
{
    public static bool IsAdmin(this ClaimsPrincipal user)
        => user.IsInRole(UserRoles.Admin)
           || user.HasClaim(ClaimTypes.Role, UserRoles.Admin);

    public static bool IsOwnerOf(this ClaimsPrincipal user, string? ownerId)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return !string.IsNullOrEmpty(userId)
               && !string.IsNullOrEmpty(ownerId)
               && string.Equals(userId, ownerId, StringComparison.Ordinal);
    }
}
