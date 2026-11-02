using CookBook.Api.BL.Models;
using CookBook.Common.BL.Facades;
using CookBook.Common.Models;

namespace CookBook.Api.BL.Facades
{
    public interface IIngredientFacade : IAppFacade
    {
        List<IngredientListModel> GetAll();
        IngredientDetailModel? GetById(Guid id);
        ResourceOwnership? GetOwnership(Guid id);
        Guid Create(IngredientDetailModel ingredientModel, string? ownerId);
        Guid? Update(IngredientDetailModel ingredientModel);
        void Delete(Guid id);
    }
}
