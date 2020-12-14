using CookBook.BL.Mobile.Services;
using CookBook.BL.Mobile.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookBook.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        private readonly INavigation navigation;
        private readonly IMvvmLocatorService mvvmLocatorService;

        public NavigationService(INavigation navigation, IMvvmLocatorService mvvmLocatorService)
        {
            this.navigation = navigation;
            this.mvvmLocatorService = mvvmLocatorService;
        }

        public async Task PushAsync<TViewModel>(TViewModel viewModel = default, bool animated = default, bool clearHistory = default) where TViewModel : class, IViewModel
        {
            try
            {
                var view = mvvmLocatorService.ResolveView(viewModel);
                await navigation.PushAsync(view, animated);
            }
            catch
            {
                // ignored
            }
        }

        public async Task PushAsync<TViewModel, TViewModelParameter>(TViewModelParameter viewModelParameter = default,
            TViewModel viewModel = default, bool animated = default, bool clearHistory = default) where TViewModel : class, IViewModel<TViewModelParameter>
        {
            try
            {
                var view = mvvmLocatorService.ResolveView(viewModel, viewModelParameter);
                await navigation.PushAsync(view, animated);
            }
            catch
            {
                // ignored
            }
        }

        public async Task PopAsync(bool animated = default)
        {
            await navigation.PopAsync(animated);
        }

        public async Task PopToRootAsync(bool animated = default)
        {
            await navigation.PopToRootAsync(animated);
        }
    }
}