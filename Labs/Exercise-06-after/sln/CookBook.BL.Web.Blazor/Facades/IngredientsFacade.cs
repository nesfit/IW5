using System;
using CookBook.BL.Web.Blazor.Api;
using CookBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Blazor.Facades
{
    public class IngredientsFacade : FacadeBase, IIngredientsFacade
    {
        private readonly IIngredientClient ingredientClient;
        const string apiVersion = "3";
        const string culture = "cs";

        public IngredientsFacade(IIngredientClient ingredientClient)
        {
            this.ingredientClient = ingredientClient;
        }

        public async Task<IList<IngredientListModel>> IngredientsGetAsync()
        {
            return await ingredientClient.IngredientGetAsync(apiVersion, culture);
        }

        public async Task<IngredientDetailModel> IngredientGetAsync(Guid id)
        {
            return await ingredientClient.IngredientGetAsync(id, apiVersion, culture);
        }

        public async Task<Guid> SaveAsync(IngredientDetailModel data)
        {
            if (data.Id==Guid.Empty)
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