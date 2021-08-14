using System;
using System.Collections.Generic;
using CookBook.Api.DAL.Common.Entities;

namespace CookBook.Api.DAL.Common.Repositories
{
    public interface IIngredientAmountRepository : IApiRepository<IngredientAmountEntity>
    {
        IList<IngredientAmountEntity> GetByRecipeId(Guid recipeId);
        IngredientAmountEntity? GetByRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId);
    }
}
