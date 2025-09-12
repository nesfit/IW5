using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class RecipeRepository : RepositoryBase<RecipeEntity>, IRecipeRepository
    {
        public RecipeRepository(CookBookDbContext dbContext)
            : base(dbContext)
        {
        }

        public override RecipeEntity? GetById(Guid id)
        {
            return dbContext.Recipes
                .Include(recipe => recipe.IngredientAmounts)
                .ThenInclude(ingredientAmount => ingredientAmount.Ingredient)
                .SingleOrDefault(entity => entity.Id == id);
        }

        public override Guid? Update(RecipeEntity recipe)
        {
            if (Exists(recipe.Id))
            {
                var existingRecipe = dbContext.Recipes
                    .Include(r => r.IngredientAmounts)
                    .Single(r => r.Id == recipe.Id);

                recipe.Adapt(existingRecipe);
                
                dbContext.Recipes.Update(existingRecipe);
                dbContext.SaveChanges();

                return existingRecipe.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
