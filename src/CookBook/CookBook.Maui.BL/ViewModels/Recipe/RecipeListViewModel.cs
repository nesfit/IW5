using CookBook.Common.Models;

namespace CookBook.Maui.BL.ViewModels;

public class RecipeListViewModel : ListViewModelBase<RecipeDetailModel, RecipeListModel>
{
    public RecipeListViewModel(Dependencies dependencies)
        : base(dependencies)
    {
    }
}
