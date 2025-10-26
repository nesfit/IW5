using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public interface IRecipeFacade : IAppFacade
{
    List<RecipeListModel> GetAll();
    RecipeDetailModel? GetById(Guid id);
    Guid CreateOrUpdate(RecipeDetailModel recipeModel, IList<string> userRoles, string? ownerId = null);
    Guid Create(RecipeDetailModel recipeModel, IList<string> userRoles, string? ownerId = null);
    Guid? Update(RecipeDetailModel recipeModel, IList<string> userRoles, string? ownerId = null);
    void Delete(Guid id, IList<string> userRoles, string? ownerId = null);
}
