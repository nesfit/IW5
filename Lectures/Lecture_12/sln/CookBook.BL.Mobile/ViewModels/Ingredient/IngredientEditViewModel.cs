using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookBook.BL.Mobile.ViewModels
{
    public class IngredientEditViewModel : ViewModelBase<Guid?>
    {
        private readonly IngredientsFacade ingredientsFacade;
        private readonly INavigationService navigationService;

        public IngredientDetailModel Ingredient { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public IngredientEditViewModel(Guid? viewModelParameter,
            IngredientsFacade ingredientsFacade,
            ICommandFactory commandFactory,
            INavigationService navigationService)
            : base(viewModelParameter)
        {
            this.ingredientsFacade = ingredientsFacade;
            this.navigationService = navigationService;
            CancelCommand = commandFactory.CreateCommand(Cancel);
            SaveCommand = commandFactory.CreateCommand(Save);
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            if (viewModelParameter == null)
            {
                Ingredient = new IngredientDetailModel { Id = Guid.Empty };
            }
            else
            {
                Ingredient = await ingredientsFacade.GetIngredientAsync(viewModelParameter.Value);
            }
        }

        private async void Save()
        {
            await ingredientsFacade.SaveAsync(Ingredient);
            await navigationService.PopAsync();
        }

        private async void Cancel()
        {
            await navigationService.PopAsync();
        }
    }
}