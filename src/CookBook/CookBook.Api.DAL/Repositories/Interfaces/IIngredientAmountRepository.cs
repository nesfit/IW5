using System;
using System.Collections.Generic;
using CookBook.DAL.Api.Entities;

namespace CookBook.DAL.Api.Repositories
{
    public interface IIngredientAmountRepository : IApiRepository<IngredientAmountEntity>
    {
        IList<IngredientAmountEntity> GetByRecipeId(Guid recipeId);
        IngredientAmountEntity? GetByRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId);
    }
}
