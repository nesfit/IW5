using CookBook.BL.Mobile.ViewModels;
using System.Threading.Tasks;

namespace CookBook.BL.Mobile.Services
{
    public interface INavigationService : ISingletonService
    {
        Task PushAsync<TViewModel>(TViewModel viewModel = default, bool animated = default, bool clearHistory = default)
            where TViewModel : class, IViewModel;
        Task PopAsync(bool animated = default);
        Task PopToRootAsync(bool animated = default);
    }
}