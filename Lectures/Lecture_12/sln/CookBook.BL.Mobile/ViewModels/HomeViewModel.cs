using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using System.Windows.Input;

namespace CookBook.BL.Mobile.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;
        private readonly ICommandFactory commandFactory;

        public ICommand NavigateToRecipesCommand { get; set; }
        public ICommand NavigateToIngredientsCommand { get; set; }

        public HomeViewModel(INavigationService navigationService,
                ICommandFactory commandFactory)
        {
            this.navigationService = navigationService;
            this.commandFactory = commandFactory;


            NavigateToRecipesCommand = commandFactory.CreateCommand(NavigateToRecipes);
            NavigateToIngredientsCommand = commandFactory.CreateCommand(NavigateToIngredients);
        }

        private void NavigateToRecipes()
        {
        }

        private void NavigateToIngredients()
        {
            navigationService.PushAsync<IngredientsListViewModel>();
        }
    }
}