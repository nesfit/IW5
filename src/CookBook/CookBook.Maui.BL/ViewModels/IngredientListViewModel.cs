using CookBook.Common.Models;
using CookBook.Maui.BL.Facades;

namespace CookBook.Maui.BL.ViewModels;

public class IngredientListViewModel : ViewModelBase
{
    private readonly IIngredientFacade ingredientFacade;

    public List<IngredientListModel> Items { get; set; }

    public IngredientListViewModel(IIngredientFacade ingredientFacade)
    {
        this.ingredientFacade = ingredientFacade;
    }

    public async Task OnAppearingAsync()
    {
        Items = await ingredientFacade.GetAllAsync();
    }
}
