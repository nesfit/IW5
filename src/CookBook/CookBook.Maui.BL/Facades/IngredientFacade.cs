using CookBook.Common.Models;

namespace CookBook.Maui.BL.Facades
{
    public class IngredientFacade : IIngredientFacade
    {
        public async Task<List<IngredientListModel>> GetAllAsync()
        {
            return new List<IngredientListModel>
            {
                new(Guid.NewGuid(), "Test 1"),
                new(Guid.NewGuid(), "Test 2"),
                new(Guid.NewGuid(), "Test 3"),
                new(Guid.NewGuid(), "Test 4"),
            };
        }
    }
}
