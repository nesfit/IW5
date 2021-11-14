using CookBook.Common.Models;

namespace CookBook.Maui.BL.ViewModels;

public class IngredientListViewModel : ListViewModelBase<IngredientDetailModel, IngredientListModel>
{
    public IngredientListViewModel(Dependencies dependencies)
        : base(dependencies)
    {
    }
}
