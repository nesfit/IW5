using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public interface IRecipeFacade : IAppFacade
{
    List<RecipeListModel> GetAll();
    RecipeDetailModel? GetById(Guid id);
    Guid CreateOrUpdate(RecipeDetailModel recipeModel, string? ownerId = null);
    Guid Create(RecipeDetailModel recipeModel, string? ownerId = null);
    Guid? Update(RecipeDetailModel recipeModel, string? ownerId = null);
    void Delete(Guid id, string? ownerId = null);
}
