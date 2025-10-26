using System.Security.Claims;

public abstract class EndpointsBase
{
    abstract public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpointRouteBuilder);

    public string? GetUserId(IHttpContextAccessor httpContextAccessor)
        => httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    public bool IsUserInRole(IHttpContextAccessor httpContextAccessor, string role)
        => httpContextAccessor.HttpContext?.User.IsInRole(role)
            ?? false;
}
