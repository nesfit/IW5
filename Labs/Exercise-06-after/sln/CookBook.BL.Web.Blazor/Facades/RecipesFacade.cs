using System;
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
        public async Task<RecipeDetailModel> RecipeGetAsync(Guid id, string culture)
        {
            return await recipeClient.RecipeGetAsync(id, apiVersion, culture);
        }

        public async Task<Guid> SaveAsync(RecipeDetailModel data, string culture)
        {
            if (data.Id == Guid.Empty)
            {
                return await recipeClient.RecipePostAsync(apiVersion, culture, data);
            }
            else
            {
                var recipePutAsync = await recipeClient.RecipePutAsync(apiVersion, culture, data);
                return recipePutAsync ?? Guid.Empty;
            }
        }

        public async Task DeleteAsync(Guid id, string culture)
        {
            await recipeClient.RecipeDeleteAsync(id, apiVersion, culture);
        }
    }
}