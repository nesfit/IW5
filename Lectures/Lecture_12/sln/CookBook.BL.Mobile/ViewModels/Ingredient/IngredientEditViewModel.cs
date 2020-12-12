using CookBook.BL.Mobile.Facades;
using System;
using System.Threading.Tasks;

namespace CookBook.BL.Mobile.ViewModels
{
    public class IngredientEditViewModel : ViewModelBase<Guid?>
    {
        private readonly IngredientsFacade ingredientsFacade;

        public IngredientEditViewModel(Guid? viewModelParameter,
            IngredientsFacade ingredientsFacade)
            : base(viewModelParameter)
        {
            this.ingredientsFacade = ingredientsFacade;
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();
        }
    }
}