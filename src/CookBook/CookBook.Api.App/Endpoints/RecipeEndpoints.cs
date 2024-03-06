using System;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using CookBook.Common.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;

namespace CookBook.Api.App.Endpoints;

public static class RecipeEndpoints
{
    public static void UseRecipeEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var recipeEndpoints = endpointRouteBuilder.MapGroup("recipe")
            .WithTags("recipe");

        recipeEndpoints.MapGet("", (IRecipeFacade recipeFacade) => recipeFacade.GetAll());

        recipeEndpoints.MapGet("{id:guid}", Results<Ok<RecipeDetailModel>, NotFound<string>> (Guid id, IRecipeFacade recipeFacade, IStringLocalizer<RecipeEndpointsResources> recipeEndpointsLocalizer)
            => recipeFacade.GetById(id) is { } recipe
                ? TypedResults.Ok(recipe)
                : TypedResults.NotFound(recipeEndpointsLocalizer[nameof(RecipeEndpointsResources.GetById_NotFound), id].Value));

        var recipeModifyingEndpoints = recipeEndpoints.MapGroup("")
            .RequireAuthorization()
            ;

        recipeModifyingEndpoints.MapPost("", (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = EndpointsBase.GetUserId(httpContextAccessor);
            return recipeFacade.Create(recipe, userId);
        });

        recipeModifyingEndpoints.MapPut("", Results<Ok<Guid?>, ForbidHttpResult> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = EndpointsBase.GetUserId(httpContextAccessor);
            try
            {
                return TypedResults.Ok(recipeFacade.Update(recipe, userId));
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        });

        recipeModifyingEndpoints.MapDelete("{id:guid}", Results<Ok, ForbidHttpResult> (Guid id, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = EndpointsBase.GetUserId(httpContextAccessor);
            try
            {
                recipeFacade.Delete(id, userId);
                return TypedResults.Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        });
    }
}
