using CookBook.Api.App.Filters;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using CookBook.Common.Resources;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Localization;

namespace CookBook.Api.App.Endpoints;

public class RecipeEndpoints : EndpointsBase
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

        var recipeModifyingEndpoints = recipeEndpoints.MapGroup("")
            .RequireAuthorization();

        recipeModifyingEndpoints.MapPost("", (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
            return recipeFacade.Create(recipe, userId);
        }).AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        recipeModifyingEndpoints.MapPut("", Results<Ok<Guid?>, ForbidHttpResult> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
            try
            {
                return TypedResults.Ok(recipeFacade.Update(recipe, userId));
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        }).AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        recipeEndpoints.MapPost("upsert", async Task<Results<Ok<Guid>, ValidationProblem>> (RecipeDetailModel recipe, IRecipeFacade recipeFacade, IValidator<RecipeDetailModel> validator) =>
        {
            var id = recipeFacade.CreateOrUpdate(recipe);
            return TypedResults.Ok(id);
        }).AddEndpointFilter<ValidationFilter<RecipeDetailModel>>();

        recipeModifyingEndpoints.MapDelete("{id:guid}", Results<Ok, ForbidHttpResult> (Guid id, IRecipeFacade recipeFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
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

        return endpointRouteBuilder;
    }
}
