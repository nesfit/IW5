using System.Threading.Tasks;

namespace Contacts.ViewModels.UIServices
{
    public interface INavigationService
    {
        void OpenWindow(Window windowKey, object parameter = null);

        Task<bool?> OpenWindowDialog(Window windowKey, object parameter = null);
    }
}