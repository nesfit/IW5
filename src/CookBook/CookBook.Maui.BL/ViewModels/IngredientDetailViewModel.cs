using CookBook.Common.Models;
using CookBook.Maui.BL.Facades;

namespace CookBook.Maui.BL.ViewModels;

public class IngredientDetailViewModel : ViewModelWithParameterBase<Guid?>
{
    private readonly IIngredientFacade ingredientFacade;
    public IngredientDetailModel Item { get; set; }

    public IngredientDetailViewModel(IIngredientFacade ingredientFacade)
    {
        this.ingredientFacade = ingredientFacade;
    }

    public override async Task OnAppearingAsync()
    {
        if (Parameter is not null)
        {
            Item = await ingredientFacade.GetByIdAsync(Parameter.Value);
        }
    }
}
