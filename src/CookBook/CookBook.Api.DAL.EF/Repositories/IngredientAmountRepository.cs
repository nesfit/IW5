using System;
using System.Collections.Generic;
using System.Linq;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class IngredientAmountRepository : RepositoryBase<IngredientAmountEntity>, IIngredientAmountRepository
    {
        public IngredientAmountRepository(CookBookDbContext dbContext)
            : base(dbContext)
        {
        }

        public IList<IngredientAmountEntity> GetByRecipeId(Guid recipeId)
        {
            return dbContext.IngredientAmounts
                .Where(entity => entity.RecipeId == recipeId)
                .ToList();
        }

        public IngredientAmountEntity? GetByRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId)
        {
            return dbContext.IngredientAmounts
                .SingleOrDefault(entity => entity.RecipeId == recipeId && entity.IngredientId == ingredientId);
        }
    }
}
