using System.Security.Claims;

public abstract class EndpointsBase
{
    abstract public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpointRouteBuilder);
    
    public string? GetUserId(IHttpContextAccessor httpContextAccessor)
    {
        var idClaim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
        return idClaim?.Value;
    }
}
