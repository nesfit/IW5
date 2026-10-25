using System.Security.Claims;
using CookBook.Api.App.Authorization;
using CookBook.Api.App.Filters;
using CookBook.Api.App.Resources.Texts;
using CookBook.Api.BL.Facades;
using CookBook.Common.Models;
using CookBook.Common.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace CookBook.Api.App.Endpoints;

public class IngredientEndpoints(IOptions<IdentityOptions> identityOptions)
    : EndpointsBase(identityOptions)
{
    public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
    {
        var ingredientEndpoints = endpointRouteBuilder.MapGroup("ingredient")
            .WithTags("ingredient");

        ingredientEndpoints.MapGet("", (IIngredientFacade ingredientFacade) => ingredientFacade.GetAll());

        ingredientEndpoints.MapGet("{id:guid}", Results<Ok<IngredientDetailModel>, NotFound<string>> (Guid id, IIngredientFacade ingredientFacade)
            => ingredientFacade.GetById(id) is { } ingredient
                ? TypedResults.Ok(ingredient)
                : NotFound(id));

        var ingredientModifyingEndpoints = ingredientEndpoints.MapGroup("");

        if (IsIdentityEnabled)
        {
            ingredientModifyingEndpoints.RequireAuthorization();
        }

        ingredientModifyingEndpoints.MapPost("", (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, ClaimsPrincipal user)
            => TypedResults.Ok(ingredientFacade.Create(ingredient, GetUserId(user))))
            .AddEndpointFilter<ValidationFilter<IngredientDetailModel>>();

        ingredientModifyingEndpoints.MapPut("", async Task<Results<Ok<Guid?>, NotFound<string>, ForbidHttpResult>> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IAuthorizationService authorizationService, ClaimsPrincipal user) =>
        {
            if (ingredientFacade.GetOwnership(ingredient.Id) is not { } ownership)
            {
                return NotFound(ingredient.Id);
            }

            if (!await IsAuthorizedAsync(authorizationService, user, ownership, OwnerOrAdminRequirement.Instance))
            {
                return TypedResults.Forbid();
            }

            return TypedResults.Ok(ingredientFacade.Update(ingredient));
        }).AddEndpointFilter<ValidationFilter<IngredientDetailModel>>();

        ingredientModifyingEndpoints.MapPost("upsert", async Task<Results<Ok<Guid>, ProblemHttpResult>> (IngredientDetailModel ingredient, IIngredientFacade ingredientFacade, IAuthorizationService authorizationService, ClaimsPrincipal user) =>
        {
            if (ingredientFacade.GetOwnership(ingredient.Id) is not { } ownership)
            {
                return TypedResults.Ok(ingredientFacade.Create(ingredient, GetUserId(user)));
            }

            if (!await IsAuthorizedAsync(authorizationService, user, ownership, OwnerOrAdminRequirement.Instance))
            {
                return TypedResults.Problem(
                    statusCode: StatusCodes.Status403Forbidden,
                    title: IngredientEndpointsResources.Upsert_Forbidden_Title,
                    detail: string.Format(IngredientEndpointsResources.Upsert_Forbidden_Detail, ingredient.Id));
            }

            return TypedResults.Ok(ingredientFacade.Update(ingredient)!.Value);
        }).AddEndpointFilter<ValidationFilter<IngredientDetailModel>>();

        ingredientModifyingEndpoints.MapDelete("{id:guid}", async Task<Results<Ok, NotFound<string>, ForbidHttpResult>> (Guid id, IIngredientFacade ingredientFacade, IAuthorizationService authorizationService, ClaimsPrincipal user) =>
        {
            if (ingredientFacade.GetOwnership(id) is not { } ownership)
            {
                return NotFound(id);
            }

            if (!await IsAuthorizedAsync(authorizationService, user, ownership, AdminOnlyRequirement.Instance))
            {
                return TypedResults.Forbid();
            }

            ingredientFacade.Delete(id);
            return TypedResults.Ok();
        });

        return endpointRouteBuilder;
    }

    private static NotFound<string> NotFound(Guid id)
        => TypedResults.NotFound(string.Format(IngredientEndpointsResources.GetById_NotFound, id));
}
