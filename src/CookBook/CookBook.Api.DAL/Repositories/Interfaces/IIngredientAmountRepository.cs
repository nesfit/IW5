using System;
using System.Collections.Generic;
using CookBook.Api.DAL.Entities;

namespace CookBook.Api.DAL.Repositories
{
    public interface IIngredientAmountRepository : IApiRepository<IngredientAmountEntity>
    {
        IList<IngredientAmountEntity> GetByRecipeId(Guid recipeId);
        IngredientAmountEntity? GetByRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId);
    }
}
