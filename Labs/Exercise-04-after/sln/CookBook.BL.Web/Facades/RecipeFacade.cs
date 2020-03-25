using CookBook.BL.Common.Facades;
using CookBook.BL.Web.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Facades
{
    public class RecipeFacade : IAppFacade
    {
        private readonly IRecipeClient _recipeClient;

        public RecipeFacade(IRecipeClient recipeClient)
        {
            _recipeClient = recipeClient;
        }

        public async Task<IList<RecipeListModel>> GetAllAsync()
        {
            return (await _recipeClient.RecipeGetAsync("3", "cs")).ToList();
        }

        public async Task<RecipeDetailModel> GetByIdAsync(Guid id)
        {
            return await _recipeClient.RecipeGetAsync(id, "3", "cs");
        }

        public async Task<Guid> InsertAsync(RecipeNewModel recipeNewModel)
        {
            return await _recipeClient.RecipePostAsync("3", "cs", recipeNewModel);
        }
    }
}
