using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using CookBook.App.Commands;
using CookBook.BL;
using CookBook.BL.Messages;

namespace CookBook.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IMessenger messenger;

        public string Name { get; set; } = "Nenacteno";
        public ICommand CreateRecipeCommand { get; set; }

        public MainViewModel(IMessenger messenger)
        {
            this.messenger = messenger;
            CreateRecipeCommand = new RelayCommand(AddNewRecipe);
        }

        private void AddNewRecipe()
        {
            messenger.Send(new NewRecipeMessage());
        }

        public void OnLoad()
        {
        }
    }
}