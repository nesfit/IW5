using CookBook.BL.Web.Api;
using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Facades
{
    public class IngredientsFacade : FacadeBase
    {
        private readonly IIngredientClient ingredientClient;

        public IngredientsFacade(IIngredientClient ingredientClient)
        {
            this.ingredientClient = ingredientClient;
        }

        public async Task<ICollection<IngredientListModel>> GetIngredientsAsync()
        {
            return await ingredientClient.IngredientGetAsync(apiVersion, culture);
        }

        public async Task<IngredientDetailModel> GetIngredientAsync(Guid id)
        {
            return await ingredientClient.IngredientGetAsync(id, apiVersion, culture);
        }

        public async Task<Guid> SaveAsync(IngredientDetailModel data)
        {
            if (data.Id == Guid.Empty)
            {
                return await ingredientClient.IngredientPostAsync(apiVersion, culture, data);
            }
            else
            {
                return await ingredientClient.IngredientPutAsync(apiVersion, culture, data);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            await ingredientClient.IngredientDeleteAsync(id, apiVersion, culture);
        }
    }
}
