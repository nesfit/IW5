using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public interface IIngredientFacade : IAppFacade
    {
        List<IngredientListModel> GetAll();
        IngredientDetailModel? GetById(Guid id);
        Guid CreateOrUpdate(IngredientDetailModel ingredientModel, string? ownerId = null);
        Guid Create(IngredientDetailModel ingredientModel, string? ownerId = null);
        Guid? Update(IngredientDetailModel ingredientModel, string? ownerId = null);
        void Delete(Guid id, string? ownerId = null);
    }
}
