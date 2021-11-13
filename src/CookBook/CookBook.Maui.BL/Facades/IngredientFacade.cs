using CookBook.Common.Models;
using CookBook.Maui.BL.Api;

namespace CookBook.Maui.BL.Facades
{
    public class IngredientFacade : IIngredientFacade
    {
        private readonly IIngredientApiClient ingredientApiClient;

        public IngredientFacade(IIngredientApiClient ingredientApiClient)
        {
            this.ingredientApiClient = ingredientApiClient;
        }

        public async Task<List<IngredientListModel>> GetAllAsync()
        {
            return (await ingredientApiClient.IngredientGetAsync("3.0", "en")).ToList();
        }
    }
}
