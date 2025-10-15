using System.Security.Claims;

public class EndpointsBase
{
    public string? GetUserId(IHttpContextAccessor httpContextAccessor)
    {
        var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        return idClaim?.Value;
    }
}
