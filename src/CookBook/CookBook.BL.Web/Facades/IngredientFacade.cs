using AutoMapper;
using CookBook.ApiClients;
using CookBook.DAL.Web.Repositories;
using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Facades
{
    public class IngredientFacade : FacadeBase<IngredientDetailModel, IngredientListModel>
    {
        private readonly IIngredientClient ingredientClient;

        public IngredientFacade(
            IIngredientClient ingredientClient,
            IngredientRepository ingredientRepository,
            IMapper mapper)
            : base(ingredientRepository, mapper)
        {
            this.ingredientClient = ingredientClient;
        }

        public async Task<ICollection<IngredientListModel>> GetIngredientsAsync()
        {
            return await ingredientClient.IngredientGetAsync(apiVersion, culture);
        }

        public override async Task<List<IngredientListModel>> GetAllAsync()
        {
            var ingredientsAll = await base.GetAllAsync();

            var ingredientsFromApi = await ingredientClient.IngredientGetAsync(apiVersion, culture);
            ingredientsAll.AddRange(ingredientsFromApi);

            return ingredientsAll;
        }

        public override async Task<IngredientDetailModel> GetByIdAsync(Guid id)
        {
            return await ingredientClient.IngredientGetAsync(id, apiVersion, culture);
        }

        public override async Task<Guid> SaveToApiAsync(IngredientDetailModel data)
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

        public override async Task DeleteAsync(Guid id)
        {
            await ingredientClient.IngredientDeleteAsync(id, apiVersion, culture);
        }
    }
}
