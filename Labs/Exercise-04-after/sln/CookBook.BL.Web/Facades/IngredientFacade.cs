using CookBook.BL.Common.Facades;
using CookBook.BL.Web.Api;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Facades
{
    public class IngredientFacade : IAppFacade
    {
        private readonly IIngredientClient _ingredientClient;

        public IngredientFacade(IIngredientClient ingredientClient)
        {
            _ingredientClient = ingredientClient;
        }

        public async Task<IList<IngredientListModel>> GetAllAsync()
        {
            return (await _ingredientClient.IngredientGetAsync("3", "cs")).ToList();
        }
    }
}
