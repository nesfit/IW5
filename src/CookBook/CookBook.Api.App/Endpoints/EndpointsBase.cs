using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CookBook.Api.App.Endpoints;

public static class EndpointsBase
{
    public static string? GetUserId(IHttpContextAccessor httpContextAccessor)
    {
        var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        return idClaim?.Value;
    }
}
