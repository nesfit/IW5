using CookBook.Api.App.Filters;
using CookBook.Api.BL.Facades;
using CookBook.Common;
using CookBook.Common.Models;
using CookBook.Common.Options;
using CookBook.Common.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace CookBook.Api.App.Endpoints;

public class RecipeEndpoints(IOptions<IdentityOptions> identityOptions) : EndpointsBase
{
    public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var recipeEndpoints = endpointRouteBuilder.MapGroup("recipe")
            .WithTags("recipe");

        recipeEndpoints.MapGet("", (IRecipeFacade recipeFacade) => recipeFacade.GetAll());

        recipeEndpoints.MapGet("{id:guid}", Results<Ok<RecipeDetailModel>, NotFound<string>> (Guid id, IRecipeFacade recipeFacade, IStringLocalizer<RecipeEndpointsResources> recipeEndpointsLocalizer)
            => recipeFacade.GetById(id) is { } recipe
                ? TypedResults.Ok(recipe)
                : TypedResults.NotFound(recipeEndpointsLocalizer[nameof(RecipeEndpointsResources.GetById_NotFound), id].Value));

        var recipeModifyingEndpoints = recipeEndpoints.MapGroup("");

        if(identityOptions.Value.IsIdentityEnabled)
        {
            recipeModifyingEndpoints.RequireAuthorization();
        }

        recipeModifyingEndpoints.MapPost("", (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
            var userRoles = GetUserRoles(httpContextAccessor);

            return recipeFacade.Create(recipe, userRoles, userId);
        }).AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        recipeModifyingEndpoints.MapPut("", Results<Ok<Guid?>, ForbidHttpResult> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
            var userRoles = GetUserRoles(httpContextAccessor);

            try
            {
                return TypedResults.Ok(recipeFacade.Update(recipe, userRoles, userId));
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        }).AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        recipeEndpoints.MapPost("upsert", async Task<Results<Ok<Guid>, ValidationProblem>> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IValidator<RecipeDetailModel> validator, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
            var userRoles = GetUserRoles(httpContextAccessor);

            var id = recipeFacade.CreateOrUpdate(recipe, userRoles, userId);
            return TypedResults.Ok(id);
        }).AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        var recipeDeleteEndpoint = recipeModifyingEndpoints.MapDelete("{id:guid}", Results<Ok, ForbidHttpResult> (Guid id, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
            var userRoles = GetUserRoles(httpContextAccessor);
            
            try
            {
                recipeFacade.Delete(id, userRoles, userId);
                return TypedResults.Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        });

        if (identityOptions.Value.IsIdentityEnabled)
        {
            // // Uncomment this to only enable admins to delete recipes
            // recipeDeleteEndpoint.RequireAuthorization(ApiPolicies.RecipeAdmin);
        }

        return endpointRouteBuilder;
    }
}
