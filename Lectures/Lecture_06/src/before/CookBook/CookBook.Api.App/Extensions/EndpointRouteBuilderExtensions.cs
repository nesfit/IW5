using CookBook.Api.App.Endpoints;
using CookBook.Common.Options;
using Microsoft.Extensions.Options;

namespace CookBook.Api.App.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder UseIngredientEndpoints(
        this IEndpointRouteBuilder endpointRouteBuilder,
        IOptions<IdentityOptions> identityOptions)
        => new IngredientEndpoints(identityOptions)
        .MapEndpoints(endpointRouteBuilder);

    public static IEndpointRouteBuilder UseRecipeEndpoints(
        this IEndpointRouteBuilder endpointRouteBuilder,
        IOptions<IdentityOptions> identityOptions)
        => new RecipeEndpoints(identityOptions)
        .MapEndpoints(endpointRouteBuilder);
}
