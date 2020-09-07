using CookBook.BL.Common.Facades;
using CookBook.BL.Web.Api;
using CookBook.BL.Web.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Models.Ingredient;

namespace CookBook.BL.Web.Facades
{
    public class IngredientFacade : IAppFacade
    {
        private readonly IIngredientClient _ingredientClient;
        private readonly IOptions<ApiOptions> apiOptions;
        private readonly IOptions<WebOptions> webOptions;

        public IngredientFacade(IIngredientClient ingredientClient,
            IOptions<ApiOptions> apiOptions,
            IOptions<WebOptions> webOptions)
        {
            _ingredientClient = ingredientClient;
            this.apiOptions = apiOptions;
            this.webOptions = webOptions;
        }

        public async Task<IList<IngredientListModel>> GetAllAsync()
        {
            return (await _ingredientClient.IngredientGetAsync("3", "cs")).ToList();
        }

        public async Task<IngredientDetailModel> GetByIdAsync(Guid id)
        {
            return await _ingredientClient.IngredientGetAsync(id, "3", "cs");
        }

        public async Task<Guid> InsertAsync(IngredientNewModel ingredientNewModel)
        {
            return await _ingredientClient.IngredientPostAsync("3", "cs", ingredientNewModel);
        }

        public async Task DeleteAsync(Guid id, string version = null, string culture = null)
        {
            version ??= apiOptions.Value.Version;
            culture ??= webOptions.Value.DefaultCulture;
            await _ingredientClient.IngredientDeleteAsync(id, version, culture);
        }
    }
}
