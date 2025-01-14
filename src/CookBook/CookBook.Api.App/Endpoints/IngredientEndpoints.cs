using System;
using CookBook.Api.App.Filters;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using CookBook.Common.Options;
using CookBook.Common.Resources;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;

namespace CookBook.Api.App.Endpoints;

public static class IngredientEndpoints
{
    public static void UseIngredientEndpoints(this IEndpointRouteBuilder endpointRouteBuilder, IdentityOptions identityOptions)
    {
        var ingredientEndpoints = endpointRouteBuilder.MapGroup("ingredient")
            .WithTags("ingredient");

        ingredientEndpoints.MapGet("", (IIngredientFacade ingredientFacade) => ingredientFacade.GetAll());

        ingredientEndpoints.MapGet("{id:guid}", Results<Ok<IngredientDetailModel>, NotFound<string>> (Guid id, IIngredientFacade ingredientFacade, IStringLocalizer<IngredientEndpointsResources> ingredientEndpointsLocalizer)
            => ingredientFacade.GetById(id) is { } ingredient
                ? TypedResults.Ok(ingredient)
                : TypedResults.NotFound(ingredientEndpointsLocalizer[nameof(IngredientEndpointsResources.GetById_NotFound), id].Value));

        var ingredientModifyingEndpoints = ingredientEndpoints.MapGroup("");
        if (identityOptions.IsEnabled)
        {
            ingredientModifyingEndpoints.RequireAuthorization();
        }

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

        ingredientModifyingEndpoints.MapPost("upsert", Results<Ok<Guid>, ForbidHttpResult> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor)
            =>
        {
            var userId = EndpointsBase.GetUserId(httpContextAccessor);

            try
            {
                return TypedResults.Ok(ingredientFacade.CreateOrUpdate(ingredient, userId));
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
        });

        var deleteEndpointRouteHandlerBuilder = ingredientModifyingEndpoints.MapDelete("{id:guid}", Results<Ok, ForbidHttpResult> (Guid id, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor) =>
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
        if (identityOptions.IsEnabled)
        {
            deleteEndpointRouteHandlerBuilder.RequireAuthorization(ApiPolicies.IngredientAdmin);
        }
    }
}
