using CookBook.BL.Web.Blazor.Api;
using CookBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Blazor.Facades
{
    public class RecipesFacade : FacadeBase, IRecipesFacade
    {
        private readonly IRecipeClient recipeClient;

        public RecipesFacade(IRecipeClient recipeClient)
        {
            this.recipeClient = recipeClient;
        }

        public async Task<IList<RecipeListModel>> RecipesGetAsync(string culture)
        {
            return await recipeClient.RecipeGetAsync("3", culture);
        }
    }
}