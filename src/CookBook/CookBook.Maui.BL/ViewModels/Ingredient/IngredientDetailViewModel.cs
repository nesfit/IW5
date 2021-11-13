using System.Windows.Input;
using CookBook.Common.Models;
using CookBook.Maui.BL.Facades;
using CookBook.Maui.BL.Factories;
using CookBook.Maui.BL.Services;

namespace CookBook.Maui.BL.ViewModels;

public class IngredientDetailViewModel : ViewModelWithParameterBase<Guid?>
{
    private readonly IIngredientFacade ingredientFacade;
    private readonly INavigationService navigationService;

    public IngredientDetailModel Item { get; set; }
    public ICommand NavigateToEditPageCommand { get; set; }
    public ICommand DeleteCommand { get; set; }

    public IngredientDetailViewModel(
        IIngredientFacade ingredientFacade,
        INavigationService navigationService,
        ICommandFactory commandFactory)
    {
        this.ingredientFacade = ingredientFacade;
        this.navigationService = navigationService;

        NavigateToEditPageCommand = commandFactory.CreateCommand(NavigateToEditPageAsync);
        DeleteCommand = commandFactory.CreateCommand(Delete);
    }

    private async Task NavigateToEditPageAsync()
    {
        if (Parameter is not null)
        {
            await navigationService.PushAsync<IngredientEditViewModel, Guid?>(Parameter.Value);
        }
    }

    private async Task Delete()
    {
        if (Parameter is not null)
        {
            await ingredientFacade.DeleteAsync(Parameter.Value);
        }

        await navigationService.PopAsync();
    }

    public override async Task OnAppearingAsync()
    {
        if (Parameter is not null)
        {
            Item = await ingredientFacade.GetByIdAsync(Parameter.Value);
        }
    }
}
