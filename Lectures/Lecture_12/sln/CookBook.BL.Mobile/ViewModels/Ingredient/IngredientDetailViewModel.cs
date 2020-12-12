using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookBook.BL.Mobile.ViewModels
{
    public class IngredientDetailViewModel : ViewModelBase<Guid>
    {
        private readonly IngredientsFacade ingredientsFacade;
        private readonly INavigationService navigationService;

        public IngredientDetailModel Ingredient { get; set; }
        public ICommand NavigateToEditCommand { get; set; }

        public IngredientDetailViewModel(Guid viewModelParameter,
            IngredientsFacade ingredientsFacade,
            ICommandFactory commandFactory,
            INavigationService navigationService)
            : base(viewModelParameter)
        {
            this.ingredientsFacade = ingredientsFacade;
            this.navigationService = navigationService;
            NavigateToEditCommand = commandFactory.CreateCommand<Guid>(NavigateToEdit);
        }

        private async void NavigateToEdit(Guid id)
        {
            await navigationService.PushAsync<IngredientEditViewModel, Guid?>(id);
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            Ingredient = await ingredientsFacade.GetIngredientAsync(viewModelParameter);
        }
    }
}