using CookBook.Api.BL.Models;
using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades;

public interface IRecipeFacade : IAppFacade
{
    List<RecipeListModel> GetAll();
    RecipeDetailModel? GetById(Guid id);
    ResourceOwnership? GetOwnership(Guid id);
    Guid Create(RecipeDetailModel recipeModel, string? ownerId);
    Guid? Update(RecipeDetailModel recipeModel);
    void Delete(Guid id);
}
