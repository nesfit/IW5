using AutoMapper;
using CookBook.ApiClients;
using CookBook.DAL.Web.Repositories;
using CookBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.BL.Web.Facades
{
    public class RecipeFacade : FacadeBase<RecipeDetailModel, RecipeListModel>
    {
        private readonly IApiClient apiClient;

        public RecipeFacade(
            IApiClient apiClient,
            RecipeRepository recipeRepository,
            IMapper mapper)
            : base(recipeRepository, mapper)
        {
            this.apiClient = apiClient;
        }

        public override async Task<List<RecipeListModel>> GetAllAsync()
        {
            var recipesAll = await base.GetAllAsync();

            var recipesFromApi = await apiClient.RecipeGetAllAsync(apiVersion, culture);
            foreach (var recipeFromApi in recipesFromApi)
            {
                if (recipesAll.Any(r => r.Id == recipeFromApi.Id) is false)
                {
                    recipesAll.Add(recipeFromApi);
                }
            }

            return recipesAll;
        }

        public override async Task<RecipeDetailModel> GetByIdAsync(Guid id)
        {
            return await apiClient.RecipeGetByIdAsync(id, apiVersion, culture);
        }

        protected override async Task<Guid> SaveToApiAsync(RecipeDetailModel data)
        {
            return await apiClient.RecipeCreateOrUpdateAsync(apiVersion, culture, data);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await apiClient.RecipeDeleteAsync(id, apiVersion, culture);
        }
    }
}
