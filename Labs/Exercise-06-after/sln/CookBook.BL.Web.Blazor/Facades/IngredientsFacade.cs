using CookBook.BL.Web.Blazor.Api;
using CookBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Blazor.Facades
{
    public class IngredientsFacade : FacadeBase, IIngredientsFacade
    {
        private readonly IIngredientClient ingredientClient;

        public IngredientsFacade(IIngredientClient ingredientClient)
        {
            this.ingredientClient = ingredientClient;
        }

        public async Task<IList<IngredientListModel>> IngredientsGetAsync()
        {
            return await ingredientClient.IngredientGetAsync("3", "cs");
        }
    }
}