using System.Windows.Input;
using CookBook.Common.Models;
using CookBook.Maui.BL.Facades;
using CookBook.Maui.BL.Factories;
using CookBook.Maui.BL.Services;

namespace CookBook.Maui.BL.ViewModels;

public class IngredientEditViewModel : ViewModelWithParameterBase<Guid?>
{
    private readonly IIngredientFacade ingredientFacade;
    private readonly INavigationService navigationService;
    
    public IngredientDetailModel Item { get; set; }
    public ICommand SaveCommand { get; set; }

    public IngredientEditViewModel(
        IIngredientFacade ingredientFacade,
        INavigationService navigationService,
        ICommandFactory commandFactory)
    {
        this.ingredientFacade = ingredientFacade;
        this.navigationService = navigationService;

        SaveCommand = commandFactory.CreateCommand(SaveAsync);
    }

    private async Task SaveAsync()
    {
        await ingredientFacade.UpsertAsync(Item);
        await navigationService.PopAsync();
    }

    public override async Task OnAppearingAsync()
    {
        await base.OnAppearingAsync();

        Item = (Parameter is null)
            ? new IngredientDetailModel(Guid.NewGuid(), string.Empty, string.Empty, string.Empty)
            : await ingredientFacade.GetByIdAsync(Parameter.Value);
    }
}
