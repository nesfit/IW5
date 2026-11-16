using CookBook.IdentityProvider.BL.Facades;

namespace CookBook.IdentityProvider.App.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder UseUserEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var userEndpoints = endpointRouteBuilder.MapGroup("user");

        userEndpoints.MapGet("search",
            async (
                IAppUserFacade appUserFacade,
                string searchString)
                => await appUserFacade.SearchAsync(searchString));

        return endpointRouteBuilder;
    }
}
