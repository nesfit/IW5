using System.Windows.Input;

namespace CookBook.Maui.BL.ViewModels;

public interface IDetailViewModel<TDetailModel, TParameter> : IViewModelWithParameter<TDetailModel, TParameter>
{
    ICommand NavigateToEditViewCommand { get; set; }
    ICommand DeleteCommand { get; set; }
}
