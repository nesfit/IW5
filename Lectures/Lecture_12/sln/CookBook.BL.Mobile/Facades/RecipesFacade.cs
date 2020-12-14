using CookBook.BL.Mobile.Api;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CookBook.BL.Mobile.Facades
{
    public class RecipesFacade : FacadeBase
    {
        private readonly IRecipeClient recipeClient;

        public RecipesFacade(IRecipeClient recipeClient)
        {
            this.recipeClient = recipeClient;
        }

        public async Task<ObservableCollection<RecipeListModel>> GetRecipesAsync()
        {
            return await recipeClient.RecipeGetAsync(apiVersion, culture);
        }

        public async Task<RecipeDetailModel> GetRecipeAsync(Guid id)
        {
            return await recipeClient.RecipeGetAsync(id, apiVersion, culture);
        }

        public async Task<Guid?> SaveAsync(RecipeDetailModel recipe)
        {
            return recipe.Id == Guid.Empty
                ? await recipeClient.RecipePostAsync(apiVersion, culture, recipe)
                : await recipeClient.RecipePutAsync(apiVersion, culture, recipe);
        }

        public async Task DeleteAsync(Guid id)
        {
            await recipeClient.RecipeDeleteAsync(id, apiVersion, culture);
        }
    }
}