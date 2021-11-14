using CookBook.Common.Models;
using CookBook.Maui.BL.Api;

namespace CookBook.Maui.BL.Facades
{
    public class IngredientFacade : FacadeBase<IngredientDetailModel, IngredientListModel>
    {
        private readonly IIngredientApiClient ingredientApiClient;

        public IngredientFacade(IIngredientApiClient ingredientApiClient)
        {
            this.ingredientApiClient = ingredientApiClient;
        }

        public override async Task<List<IngredientListModel>> GetAllAsync()
        {
            return (await ingredientApiClient.IngredientGetAsync(apiVersion, culture)).ToList();
        }

        public override async Task<IngredientDetailModel> GetByIdAsync(Guid id)
        {
            return await ingredientApiClient.IngredientGetAsync(id, apiVersion, culture);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await ingredientApiClient.IngredientDeleteAsync(id, apiVersion, culture);
        }

        public override async Task UpsertAsync(IngredientDetailModel model)
        {
            await ingredientApiClient.UpsertAsync(apiVersion, culture, model);
        }
    }
}
