using System.Security.Claims;
using CookBook.Api.BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using IdentityOptions = CookBook.Common.Options.IdentityOptions;

namespace CookBook.Api.App.Endpoints;

public abstract class EndpointsBase(IOptions<IdentityOptions> identityOptions)
{
    public abstract IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpointRouteBuilder);

    protected bool IsIdentityEnabled => identityOptions.Value.IsEnabled;

    protected static string? GetUserId(ClaimsPrincipal user)
        => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    protected async Task<bool> IsAuthorizedAsync(
        IAuthorizationService authorizationService,
        ClaimsPrincipal user,
        ResourceOwnership resource,
        IAuthorizationRequirement requirement)
        => !IsIdentityEnabled
           || (await authorizationService.AuthorizeAsync(user, resource, requirement)).Succeeded;
}
