using CookBook.Common.Enums;
using CookBook.Common.Models;

namespace CookBook.Maui.BL.ViewModels;

public class RecipeEditViewModel : EditViewModelBase<RecipeDetailModel>
{
    public RecipeEditViewModel(Dependencies dependencies) : base(dependencies)
    {
    }

    protected override RecipeDetailModel GetNewItem()
    {
        return new(Guid.NewGuid(), string.Empty, string.Empty, TimeSpan.Zero, FoodType.Unknown, string.Empty);
    }
}
