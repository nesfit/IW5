using System.Security.Claims;
using CookBook.Api.App.Authorization;
using CookBook.Api.App.Filters;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using CookBook.Common.Options;
using CookBook.Common.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace CookBook.Api.App.Endpoints;

public class RecipeEndpoints(IOptions<IdentityOptions> identityOptions)
    : EndpointsBase(identityOptions)
{
    public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var recipeEndpoints = endpointRouteBuilder.MapGroup("recipe")
            .WithTags("recipe");

        recipeEndpoints.MapGet("", (IRecipeFacade recipeFacade) => recipeFacade.GetAll());

        recipeEndpoints.MapGet("{id:guid}", Results<Ok<RecipeDetailModel>, NotFound<string>> (Guid id, IRecipeFacade recipeFacade, IStringLocalizer<RecipeEndpointsResources> recipeEndpointsLocalizer)
            => recipeFacade.GetById(id) is { } recipe
                ? TypedResults.Ok(recipe)
                : NotFound(recipeEndpointsLocalizer, id));

        var recipeModifyingEndpoints = recipeEndpoints.MapGroup("");

        if (IsIdentityEnabled)
        {
            recipeModifyingEndpoints.RequireAuthorization();
        }

        recipeModifyingEndpoints.MapPost("", (RecipeDetailModel recipe, IRecipeFacade recipeFacade, ClaimsPrincipal user)
            => recipeFacade.Create(recipe, GetUserId(user)))
            .AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        recipeModifyingEndpoints.MapPut("", async Task<Results<Ok<Guid?>, NotFound<string>, ForbidHttpResult>> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IAuthorizationService authorizationService, ClaimsPrincipal user, IStringLocalizer<RecipeEndpointsResources> recipeEndpointsLocalizer) =>
        {
            if (recipeFacade.GetOwnership(recipe.Id) is not { } ownership)
            {
                return NotFound(recipeEndpointsLocalizer, recipe.Id);
            }

            if (!await IsAuthorizedAsync(authorizationService, user, ownership, OwnerOrAdminRequirement.Instance))
            {
                return TypedResults.Forbid();
            }

            return TypedResults.Ok(recipeFacade.Update(recipe));
        }).AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        recipeModifyingEndpoints.MapPost("upsert", async Task<Results<Ok<Guid>, ForbidHttpResult>> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IAuthorizationService authorizationService, ClaimsPrincipal user) =>
        {
            if (recipeFacade.GetOwnership(recipe.Id) is not { } ownership)
            {
                return TypedResults.Ok(recipeFacade.Create(recipe, GetUserId(user)));
            }

            if (!await IsAuthorizedAsync(authorizationService, user, ownership, OwnerOrAdminRequirement.Instance))
            {
                return TypedResults.Forbid();
            }

            return TypedResults.Ok(recipeFacade.Update(recipe)!.Value);
        }).AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        recipeModifyingEndpoints.MapDelete("{id:guid}", async Task<Results<Ok, NotFound<string>, ForbidHttpResult>> (Guid id, IRecipeFacade recipeFacade, IAuthorizationService authorizationService, ClaimsPrincipal user, IStringLocalizer<RecipeEndpointsResources> recipeEndpointsLocalizer) =>
        {
            if (recipeFacade.GetOwnership(id) is not { } ownership)
            {
                return NotFound(recipeEndpointsLocalizer, id);
            }

            if (!await IsAuthorizedAsync(authorizationService, user, ownership, OwnerOrAdminRequirement.Instance))
            {
                return TypedResults.Forbid();
            }

            recipeFacade.Delete(id);
            return TypedResults.Ok();
        });

        return endpointRouteBuilder;
    }

    private static NotFound<string> NotFound(IStringLocalizer<RecipeEndpointsResources> recipeEndpointsLocalizer, Guid id)
        => TypedResults.NotFound(recipeEndpointsLocalizer[nameof(RecipeEndpointsResources.GetById_NotFound), id].Value);
}
