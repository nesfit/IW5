using CookBook.BL.Mobile.Api;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CookBook.BL.Mobile.Facades
{
    public class IngredientsFacade : FacadeBase
    {
        private readonly IIngredientClient ingredientClient;

        public IngredientsFacade(IIngredientClient ingredientClient)
        {
            this.ingredientClient = ingredientClient;
        }

        public async Task<ObservableCollection<IngredientListModel>> GetIngredientsAsync()
        {
            return await ingredientClient.IngredientGetAsync(apiVersion, culture);
        }

        public async Task<IngredientDetailModel> GetIngredientAsync(Guid id)
        {
            return await ingredientClient.IngredientGetAsync(id, apiVersion, culture);
        }

        public async Task<Guid> SaveAsync(IngredientDetailModel ingredient)
        {
            return ingredient.Id == Guid.Empty
                ? await ingredientClient.IngredientPostAsync(apiVersion, culture, ingredient)
                : await ingredientClient.IngredientPutAsync(apiVersion, culture, ingredient);
        }

        public async Task DeleteAsync(Guid id)
        {
            await ingredientClient.IngredientDeleteAsync(id, apiVersion, culture);
        }
    }
}