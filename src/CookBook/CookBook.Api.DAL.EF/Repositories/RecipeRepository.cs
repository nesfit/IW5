using CookBook.Api.DAL.Common.Entities;
using CookBook.Api.DAL.Common.Mappers;
using CookBook.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Api.DAL.EF.Repositories
{
    public class RecipeRepository(
        RecipeMapper recipeMapper,
        CookBookDbContext dbContext)
        : RepositoryBase<RecipeEntity>(dbContext), IRecipeRepository
    {
        public override RecipeEntity? GetById(Guid id)
        {
            return dbContext.Recipes
                .Include(recipe => recipe.IngredientAmounts)
                .ThenInclude(ingredientAmount => ingredientAmount.Ingredient)
                .SingleOrDefault(entity => entity.Id == id);
        }

        public override Guid? Update(RecipeEntity recipeUpdated)
        {
            var recipeExisting = dbContext.Recipes
                    .Include(r => r.IngredientAmounts)
                    .SingleOrDefault(r => r.Id == recipeUpdated.Id);

            if (recipeExisting is not null)
            {
                recipeMapper.UpdateEntity(recipeUpdated, recipeExisting);
                dbContext.Recipes.Update(recipeExisting);
                dbContext.SaveChanges();

                return recipeExisting.Id;
            }
            else
            {
                return null;
            }
        }
    }
}
