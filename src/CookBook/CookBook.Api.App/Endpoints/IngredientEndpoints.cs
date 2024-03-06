using System;
using CookBook.Api.App.Filters;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using CookBook.Common.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;

namespace CookBook.Api.App.Endpoints;

public static class IngredientEndpoints
{
    public static void UseIngredientEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var ingredientEndpoints = endpointRouteBuilder.MapGroup("ingredient")
            .WithTags("ingredient");

        ingredientEndpoints.MapGet("", (IIngredientFacade ingredientFacade) => ingredientFacade.GetAll());

        ingredientEndpoints.MapGet("{id:guid}", Results<Ok<IngredientDetailModel>, NotFound<string>> (Guid id, IIngredientFacade ingredientFacade, IStringLocalizer<IngredientEndpointsResources> ingredientEndpointsLocalizer)
            => ingredientFacade.GetById(id) is { } ingredient
                ? TypedResults.Ok(ingredient)
                : TypedResults.NotFound(ingredientEndpointsLocalizer[nameof(IngredientEndpointsResources.GetById_NotFound), id].Value));

        var ingredientModifyingEndpoints = ingredientEndpoints.MapGroup("")
            .RequireAuthorization()
            ;

        ingredientModifyingEndpoints.MapPost("", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor)
                =>
            {
                var userId = EndpointsBase.GetUserId(httpContextAccessor);
                return TypedResults.Ok(ingredientFacade.Create(ingredient, userId));
            })
            .AddEndpointFilter<ValidationFilter<IngredientDetailModel>>(); ;

        ingredientModifyingEndpoints.MapPut("", Results<Ok<Guid?>, ForbidHttpResult> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = EndpointsBase.GetUserId(httpContextAccessor);
            try
            {
                return TypedResults.Ok(ingredientFacade.Update(ingredient, userId));
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        });

        ingredientModifyingEndpoints.MapDelete("{id:guid}", Results<Ok, ForbidHttpResult> (Guid id, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = EndpointsBase.GetUserId(httpContextAccessor);
            try
            {
                ingredientFacade.Delete(id, userId);
                return TypedResults.Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        });
    }
}
