using CookBook.Common.Models;
using CookBook.Maui.BL.Api;

namespace CookBook.Maui.BL.Facades
{
    public class IngredientFacade : IIngredientFacade
    {
        private const string apiVersion = "3.0";
        private const string culture = "en";

        private readonly IIngredientApiClient ingredientApiClient;

        public IngredientFacade(IIngredientApiClient ingredientApiClient)
        {
            this.ingredientApiClient = ingredientApiClient;
        }

        public async Task<List<IngredientListModel>> GetAllAsync()
        {
            return (await ingredientApiClient.IngredientGetAsync(apiVersion, culture)).ToList();
        }

        public async Task<IngredientDetailModel> GetByIdAsync(Guid id)
        {
            return await ingredientApiClient.IngredientGetAsync(id, apiVersion, culture);
        }

        public async Task DeleteAsync(Guid id)
        {
            await ingredientApiClient.IngredientDeleteAsync(id, apiVersion, culture);
        }

        public async Task UpsertAsync(IngredientDetailModel ingredient)
        {
            await ingredientApiClient.UpsertAsync(apiVersion, culture, ingredient);
        }
    }
}
