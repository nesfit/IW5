using CookBook.BL.Common.Facades;
using CookBook.BL.Web.Api;
using CookBook.BL.Web.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Facades
{
    public class RecipeFacade : IAppFacade
    {
        private readonly IRecipeClient _recipeClient;
        private readonly IOptions<ApiOptions> apiOptions;
        private readonly IOptions<WebOptions> webOptions;

        public RecipeFacade(
            IRecipeClient recipeClient,
            IOptions<ApiOptions> apiOptions,
            IOptions<WebOptions> webOptions)
        {
            _recipeClient = recipeClient;
            this.apiOptions = apiOptions;
            this.webOptions = webOptions;
        }

        public async Task<IList<RecipeListModel>> GetAllAsync()
        {
            return (await _recipeClient.RecipeGetAsync("3", "cs")).ToList();
        }

        public async Task<RecipeDetailModel> GetByIdAsync(Guid id)
        {
            return await _recipeClient.RecipeGetAsync(id, "3", "cs");
        }

        public async Task<Guid> InsertAsync(RecipeNewModel recipeNewModel, string version = null, string culture = null)
        {
            version ??= apiOptions.Value.Version;
            culture ??= webOptions.Value.DefaultCulture;
            return await _recipeClient.RecipePostAsync(version, culture, recipeNewModel);
        }
    }
}
