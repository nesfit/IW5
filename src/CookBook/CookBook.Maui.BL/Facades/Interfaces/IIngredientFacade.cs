using CookBook.Common.Models;

namespace CookBook.Maui.BL.Facades;

public interface IIngredientFacade
{
    Task<List<IngredientListModel>> GetAllAsync();
    Task<IngredientDetailModel> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task UpsertAsync(IngredientDetailModel ingredient);
}
