using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public interface IIngredientFacade : IAppFacade
    {
        List<IngredientListModel> GetAll();
        IngredientDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(IngredientDetailModel ingredientModel, IList<string> userRoles, string? ownerId = null);
        Guid Create(IngredientDetailModel ingredientModel, IList<string> userRoles, string? ownerId = null);
        Guid? Update(IngredientDetailModel ingredientModel, IList<string> userRoles, string? ownerId = null);
        void Delete(Guid id, IList<string> userRoles, string? ownerId = null);
    }
}
