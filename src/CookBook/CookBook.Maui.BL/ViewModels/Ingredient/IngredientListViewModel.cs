using System.Windows.Input;
using CookBook.Common.Models;
using CookBook.Maui.BL.Facades;
using CookBook.Maui.BL.Factories;
using CookBook.Maui.BL.Services;

namespace CookBook.Maui.BL.ViewModels;

public class IngredientListViewModel : ViewModelBase
{
    private readonly IIngredientFacade ingredientFacade;
    private readonly INavigationService navigationService;

    public ICommand ItemSelectedCommand { get; set; }

    public List<IngredientListModel> Items { get; set; }

    public IngredientListViewModel(
        IIngredientFacade ingredientFacade,
        ICommandFactory commandFactory,
        INavigationService navigationService)
    {
        this.ingredientFacade = ingredientFacade;
        this.navigationService = navigationService;
        ItemSelectedCommand = commandFactory.CreateCommand<Guid>(ItemSelectedAsync);
    }

    private void ItemSelectedAsync(Guid id)
    {
        navigationService.PushAsync<IngredientDetailViewModel, Guid?>(id);
    }

    public override async Task OnAppearingAsync()
    {
        Items = await ingredientFacade.GetAllAsync();
    }
}
