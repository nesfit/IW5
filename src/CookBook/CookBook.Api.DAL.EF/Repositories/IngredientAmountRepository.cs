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
        public IngredientAmountRepository(IDbContextFactory<CookBookDbContext> dbContextFactory)
            : base(dbContextFactory)
        {
        }

        public IList<IngredientAmountEntity> GetByRecipeId(Guid recipeId)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return dbContext.IngredientAmounts
                .Where(entity => entity.RecipeId == recipeId)
                .ToList();
        }

        public IngredientAmountEntity? GetByRecipeIdAndIngredientId(Guid recipeId, Guid ingredientId)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            return dbContext.IngredientAmounts
                .SingleOrDefault(entity => entity.RecipeId == recipeId && entity.IngredientId == ingredientId);
        }
    }
}
