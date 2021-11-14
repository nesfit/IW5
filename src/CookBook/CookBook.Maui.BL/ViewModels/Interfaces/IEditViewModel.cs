using System.Windows.Input;

namespace CookBook.Maui.BL.ViewModels;

public interface IEditViewModel<TDetailModel, TParameter> : IViewModelWithParameter<TDetailModel, TParameter>
{
    ICommand SaveCommand { get; set; }
}
