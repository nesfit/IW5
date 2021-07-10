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
        private readonly IRecipeClient recipeClient;

        public RecipeFacade(
            IRecipeClient recipeClient,
            RecipeRepository recipeRepository,
            IMapper mapper)
            : base(recipeRepository, mapper)
        {
            this.recipeClient = recipeClient;
        }

        public override async Task<List<RecipeListModel>> GetAllAsync()
        {
            var recipesAll = await base.GetAllAsync();

            var recipesFromApi = await recipeClient.RecipeGetAsync(apiVersion, culture);
            foreach (var recipeFromApi in recipesFromApi)
            {
                if (!recipesAll.Any(r => r.Id == recipeFromApi.Id))
                {
                    recipesAll.Add(recipeFromApi);
                }
            }

            return recipesAll;
        }

        public override async Task<RecipeDetailModel> GetByIdAsync(Guid id)
        {
            return await recipeClient.RecipeGetAsync(id, apiVersion, culture);
        }

        public override async Task SaveToApiAsync(RecipeDetailModel data)
        {
            if (data.Id == Guid.Empty)
            {
                await recipeClient.RecipePostAsync(apiVersion, culture, data);
            }
            else
            {
                await recipeClient.RecipePutAsync(apiVersion, culture, data);
            }
        }

        public override async Task DeleteAsync(Guid id)
        {
            await recipeClient.RecipeDeleteAsync(id, apiVersion, culture);
        }
    }
}