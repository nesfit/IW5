using CookBook.Api.App.Endpoints;

namespace CookBook.Api.App.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder UseIngredientEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        => new IngredientEndpoints().MapEndpoints(endpointRouteBuilder);

    public static IEndpointRouteBuilder UseRecipeEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        => new RecipeEndpoints().MapEndpoints(endpointRouteBuilder);
}
