using CookBook.Common.Models;
using CookBook.Maui.BL.Api;

namespace CookBook.Maui.BL.Facades;

public class RecipeFacade : FacadeBase<RecipeDetailModel, RecipeListModel>
{
    private readonly IRecipeApiClient recipeApiClient;

    public RecipeFacade(IRecipeApiClient recipeApiClient)
    {
        this.recipeApiClient = recipeApiClient;
    }

    public override async Task<List<RecipeListModel>> GetAllAsync()
    {
        return (await recipeApiClient.RecipeGetAsync(apiVersion, culture)).ToList();
    }

    public override async Task<RecipeDetailModel> GetByIdAsync(Guid id)
    {
        return await recipeApiClient.RecipeGetAsync(id, apiVersion, culture);
    }

    public override async Task DeleteAsync(Guid id)
    {
        await recipeApiClient.RecipeDeleteAsync(id, apiVersion, culture);
    }

    public override async Task UpsertAsync(RecipeDetailModel model)
    {
        await recipeApiClient.UpsertAsync(apiVersion, culture, model);
    }
}
