using CookBook.BL.Mobile.Services;
using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms;

namespace CookBook.Mobile.Services
{
    public interface IMvvmLocatorService : ISingletonService
    {
        Page ResolveView<TViewModel>(TViewModel viewModel = default)
            where TViewModel : class, IViewModel;
        Page ResolveView<TViewModel, TViewModelParameter>(TViewModel viewModel = default, TViewModelParameter viewModelParameter = default)
            where TViewModel : class, IViewModel<TViewModelParameter>;
    }
}