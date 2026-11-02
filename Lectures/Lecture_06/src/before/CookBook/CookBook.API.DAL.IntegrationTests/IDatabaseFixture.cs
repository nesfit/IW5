using System;
using System.Collections.Generic;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;

namespace CookBook.API.DAL.IntegrationTests;

public interface IDatabaseFixture
{
    IngredientAmountEntity? GetIngredientAmountDirectly(Guid ingredientAmountId);
    RecipeEntity? GetRecipeDirectly(Guid recipeId);
    IRecipeRepository GetRecipeRepository();
    IList<Guid> IngredientGuids { get; }
    IList<Guid> IngredientAmountGuids { get; }
    IList<Guid> RecipeGuids { get; }
}
