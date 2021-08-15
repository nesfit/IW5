using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CookBook.Common.Models;
using CookBook.Web.DAL.Repositories;

namespace CookBook.Web.BL.Facades
{
    public class IngredientFacade : FacadeBase<IngredientDetailModel, IngredientListModel>
    {
        private readonly IApiClient apiClient;

        public IngredientFacade(
            IApiClient apiClient,
            IngredientRepository ingredientRepository,
            IMapper mapper)
            : base(ingredientRepository, mapper)
        {
            this.apiClient = apiClient;
        }

        public override async Task<List<IngredientListModel>> GetAllAsync()
        {
            var ingredientsAll = await base.GetAllAsync();

            var ingredientsFromApi = await apiClient.IngredientGetAllAsync(apiVersion, culture);
            ingredientsAll.AddRange(ingredientsFromApi);

            return ingredientsAll;
        }

        public override async Task<IngredientDetailModel> GetByIdAsync(Guid id)
        {
            return await apiClient.IngredientGetByIdAsync(id, apiVersion, culture);
        }

        protected override async Task<Guid> SaveToApiAsync(IngredientDetailModel data)
        {
            return await apiClient.IngredientCreateOrUpdateAsync(apiVersion, culture, data);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await apiClient.IngredientDeleteAsync(id, apiVersion, culture);
        }
    }
}
