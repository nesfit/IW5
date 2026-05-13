using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Common.Models;
using CookBook.Web.BL.Api;
using CookBook.Web.BL.Mappers;
using CookBook.Web.BL.Options;
using CookBook.Web.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CookBook.Web.BL.Facades
{
    public class RecipeFacade : FacadeBase<RecipeDetailModel, RecipeListModel>
    {
        private readonly RecipeApiClient authorizedApiClient;
        private readonly RecipeApiClient anonymousApiClient;

        public RecipeFacade(
            IHttpClientFactory httpClientFactory,
            RecipeRepository recipeRepository,
            IMapper<RecipeDetailModel, RecipeListModel> mapper,
            IOptions<LocalDbOptions> localDbOptions)
            : base(recipeRepository, mapper, localDbOptions)
        {
            authorizedApiClient = new RecipeApiClient(httpClientFactory.CreateClient(ApiHttpClientNames.Default));
            anonymousApiClient = new RecipeApiClient(httpClientFactory.CreateClient(ApiHttpClientNames.Anonymous));
        }

        public override async Task<List<RecipeListModel>> GetAllAsync()
        {
            var recipesAll = await base.GetAllAsync();

            try
            {
                var recipesFromApi = await anonymousApiClient.RecipeGetAsync(culture);
                foreach (var recipeFromApi in recipesFromApi)
                {
                    if (recipesAll.Any(r => r.Id == recipeFromApi.Id) is false)
                    {
                        recipesAll.Add(recipeFromApi);
                    }
                }
            }
            catch (HttpRequestException exception) when (IsLocalDbEnabled && IsOfflineException(exception))
            {
            }

            return recipesAll;
        }

        public override async Task<RecipeDetailModel?> GetByIdAsync(Guid id)
        {
            try
            {
                return await anonymousApiClient.RecipeGetAsync(id, culture);
            }
            catch (HttpRequestException exception) when (IsLocalDbEnabled && IsOfflineException(exception))
            {
                return await GetByIdFromLocalDbAsync(id);
            }
        }

        protected override async Task<Guid> SaveToApiAsync(RecipeDetailModel data)
        {
            return await authorizedApiClient.UpsertAsync(culture, data);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await authorizedApiClient.RecipeDeleteAsync(id, culture);
        }
    }
}
