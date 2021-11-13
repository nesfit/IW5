using CookBook.Maui.BL.ViewModels;

namespace CookBook.Maui.BL.Services;

public interface INavigationService
{
    Task PushAsync<TViewModel>()
        where TViewModel : ViewModelBase;

    Task PushAsync<TViewModel, TParameter>(TParameter parameter)
        where TViewModel : ViewModelWithParameterBase<TParameter>;
}
