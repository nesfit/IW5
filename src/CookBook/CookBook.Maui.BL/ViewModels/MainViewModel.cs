using System.Windows.Input;
using CookBook.Maui.BL.Factories;
using CookBook.Maui.BL.Services;

namespace CookBook.Maui.BL.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly INavigationService navigationService;

    public ICommand NavigateToIngredientListViewCommand { get; set; }
    public ICommand NavigateToRecipeListViewCommand { get; set; }

    public MainViewModel(
        INavigationService navigationService,
        ICommandFactory commandFactory)
    {
        this.navigationService = navigationService;

        NavigateToIngredientListViewCommand = commandFactory.CreateCommand(NavigateToIngredientListViewAsync);
        NavigateToRecipeListViewCommand = commandFactory.CreateCommand(NavigateToRecipeListViewAsync);
    }

    private async Task NavigateToIngredientListViewAsync()
    {
        await navigationService.PushAsync<IngredientListViewModel>();
    }

    private async Task NavigateToRecipeListViewAsync()
    {
        await navigationService.PushAsync<RecipeListViewModel>();
    }
}
