using CookBook.BL.Mobile.Api;
using CookBook.Models;
using System.Collections.Generic;
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

        public async Task<ICollection<RecipeListModel>> GetRecipesAsync()
        {
            return await recipeClient.RecipeGetAsync(apiVersion, culture);
        }
    }
}