using CookBook.BL.Mobile.Api;
using CookBook.Models;
using System.Collections.Generic;
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

        public async Task<ICollection<IngredientListModel>> GetIngredientsAsync()
        {
            return await ingredientClient.IngredientGetAsync(apiVersion, culture);
        }
    }
}