using CookBook.Api.App.Filters;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using CookBook.Common.Resources;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CookBook.Api.App.Endpoints;

public class IngredientEndpoints : EndpointsBase
{
    public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var ingredientEndpoints = endpointRouteBuilder.MapGroup("ingredient")
            .WithTags("ingredient");

        ingredientEndpoints.MapGet("", (IIngredientFacade ingredientFacade) => ingredientFacade.GetAll());

        ingredientEndpoints.MapGet("{id:guid}", Results<Ok<IngredientDetailModel>, NotFound<string>> (Guid id, IIngredientFacade ingredientFacade)
            => ingredientFacade.GetById(id) is { } ingredient
                ? TypedResults.Ok(ingredient)
                : TypedResults.NotFound(string.Format(IngredientEndpointsResources.GetById_NotFound, id)));

        var ingredientModifyingEndpoints = ingredientEndpoints.MapGroup("")
            .RequireAuthorization();

        ingredientModifyingEndpoints.MapPost("", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor)
                =>
        {
            var userId = GetUserId(httpContextAccessor);
            return TypedResults.Ok(ingredientFacade.Create(ingredient, userId));
        }).AddEndpointFilter<ValidationFilter<IngredientDetailModel>>();

        ingredientModifyingEndpoints.MapPut("", Results<Ok<Guid?>, ForbidHttpResult> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
            try
            {
                return TypedResults.Ok(ingredientFacade.Update(ingredient, userId));
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        }).AddEndpointFilter<ValidationFilter<IngredientDetailModel>>(); ;

        ingredientModifyingEndpoints.MapPost("upsert", Results<Ok<Guid>, ForbidHttpResult> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor)
            =>
        {
            var userId = GetUserId(httpContextAccessor);

            try
            {
                return TypedResults.Ok(ingredientFacade.CreateOrUpdate(ingredient, userId));
            }
            catch (UnauthorizedAccessException)
            {

                throw;
            }
        }).AddEndpointFilter<ValidationFilter<IngredientDetailModel>>(); ;

        ingredientModifyingEndpoints.MapDelete("{id:guid}", Results<Ok, ForbidHttpResult> (Guid id, IIngredientFacade ingredientFacade, IHttpContextAccessor httpContextAccessor) =>
        {
            var userId = GetUserId(httpContextAccessor);
            try
            {
                ingredientFacade.Delete(id, userId);
                return TypedResults.Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return TypedResults.Forbid();
            }
        }).RequireAuthorization(ApiPolicies.IngredientAdmin);

        return endpointRouteBuilder;
    }
}
