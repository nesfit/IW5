using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using System.Windows.Input;

namespace CookBook.BL.Mobile.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly INavigationService navigationService;

        public ICommand NavigateToRecipesCommand { get; set; }
        public ICommand NavigateToIngredientsCommand { get; set; }

        public HomeViewModel(INavigationService navigationService,
                ICommandFactory commandFactory)
        {
            this.navigationService = navigationService;

            NavigateToRecipesCommand = commandFactory.CreateCommand(NavigateToRecipes);
            NavigateToIngredientsCommand = commandFactory.CreateCommand(NavigateToIngredients);
        }

        private void NavigateToRecipes()
        {
            navigationService.PushAsync<RecipeListViewModel>();
        }

        private void NavigateToIngredients()
        {
            navigationService.PushAsync<IngredientListViewModel>();
        }
    }
}