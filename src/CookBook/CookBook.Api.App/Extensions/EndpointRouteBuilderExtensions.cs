using CookBook.Api.App.Endpoints;

namespace CookBook.Api.App.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder UseIngredientEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        new IngredientEndpoints().MapIngredientEndpoints(endpointRouteBuilder);
        return endpointRouteBuilder;
    }
}
