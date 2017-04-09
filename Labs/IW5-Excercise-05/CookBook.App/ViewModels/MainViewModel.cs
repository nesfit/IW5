using System.Windows.Input;
using CookBook.App.Commands;
using CookBook.BL;
using CookBook.BL.Messages;

namespace CookBook.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;

        public ICommand CreateRecipeCommand { get; }

        public MainViewModel(IMessenger messenger)
        {
            _messenger = messenger;

            CreateRecipeCommand = new RelayCommand(() => _messenger.Send(new NewRecipeMessage()));
        }
    }
}