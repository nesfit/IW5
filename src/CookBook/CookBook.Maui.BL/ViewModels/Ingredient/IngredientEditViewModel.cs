using CookBook.Common.Models;

namespace CookBook.Maui.BL.ViewModels;

public class IngredientEditViewModel : EditViewModelBase<IngredientDetailModel>
{
    public IngredientEditViewModel(Dependencies dependencies)
        : base(dependencies)
    {
    }

    protected override IngredientDetailModel GetNewItem()
    {
        return new(Guid.NewGuid(), string.Empty, string.Empty);
    }
}
