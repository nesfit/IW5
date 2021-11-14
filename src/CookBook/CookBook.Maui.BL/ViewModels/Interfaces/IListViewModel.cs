using System.Windows.Input;

namespace CookBook.Maui.BL.ViewModels;

public interface IListViewModel<TListModel> : IViewModel
{
    List<TListModel> Items { get; set; }
    ICommand NavigateToDetailViewCommand { get; }
}
