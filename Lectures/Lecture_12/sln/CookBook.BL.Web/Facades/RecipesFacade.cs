using CookBook.BL.Web.Api;
using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Facades
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
        public async Task<RecipeDetailModel> GetRecipesAsync(Guid id)
        {
            return await recipeClient.RecipeGetAsync(id, apiVersion, culture);
        }

        public async Task<Guid> SaveAsync(RecipeDetailModel data)
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

        public async Task DeleteAsync(Guid id)
        {
            await recipeClient.RecipeDeleteAsync(id, apiVersion, culture);
        }
    }
}