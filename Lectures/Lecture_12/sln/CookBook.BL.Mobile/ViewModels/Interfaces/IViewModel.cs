using System.Threading.Tasks;

namespace CookBook.BL.Mobile.ViewModels
{
    public interface IViewModel
    {
        Task OnAppearing();
    }

    public interface IViewModel<TViewModelParameter> : IViewModel
    {
    }
}