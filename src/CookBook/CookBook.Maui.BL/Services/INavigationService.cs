using CookBook.Maui.BL.ViewModels;

namespace CookBook.Maui.BL.Services;

public interface INavigationService
{
    Task PushAsync<TViewModel>()
        where TViewModel : IViewModel;

    Task PushAsync<TViewModel, TDetailModel, TParameter>(TParameter parameter)
        where TViewModel : IViewModelWithParameter<TDetailModel, TParameter>;

    Task PopAsync();
}
