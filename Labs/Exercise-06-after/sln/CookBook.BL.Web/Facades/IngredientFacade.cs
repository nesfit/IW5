using CookBook.BL.Common.Facades;
using CookBook.BL.Web.Api;
using System;
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

        public async Task<IngredientDetailModel> GetByIdAsync(Guid id)
        {
            return await _ingredientClient.IngredientGetAsync(id, "3", "cs");
        }

        public async Task<Guid> InsertAsync(IngredientNewModel ingredientNewModel)
        {
            return await _ingredientClient.IngredientPostAsync("3", "cs", ingredientNewModel);
        }
    }
}
