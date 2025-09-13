using System;
using System.Collections.Generic;
using System.Linq;
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

                // Manual property copying to replace AutoMapper
                existingRecipe.Name = recipe.Name;
                existingRecipe.Description = recipe.Description;
                existingRecipe.Duration = recipe.Duration;
                existingRecipe.FoodType = recipe.FoodType;
                existingRecipe.ImageUrl = recipe.ImageUrl;
                existingRecipe.IngredientAmounts = recipe.IngredientAmounts;
                
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
