using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookBook.BL.Mobile.ViewModels
{
    public class IngredientListViewModel : ViewModelBase
    {
        private readonly IngredientsFacade ingredientsFacade;
        private readonly INavigationService navigationService;

        public ObservableCollection<IngredientListModel> Ingredients { get; set; } = new ObservableCollection<IngredientListModel>();
        public ICommand NavigateToDetailCommand { get; set; }
        public ICommand NavigateToAddCommand { get; set; }

        public IngredientListViewModel(IngredientsFacade ingredientsFacade,
            ICommandFactory commandFactory,
            INavigationService navigationService)
        {
            this.ingredientsFacade = ingredientsFacade;
            this.navigationService = navigationService;

            NavigateToDetailCommand = commandFactory.CreateCommand<Guid>(NavigateToDetail);
            NavigateToAddCommand = commandFactory.CreateCommand(NavigateToAdd);
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            try
            {
                Ingredients = await ingredientsFacade.GetIngredientsAsync();
            }
            catch (ApiException)
            {
            }
        }

        private async void NavigateToDetail(Guid id)
        {
            await navigationService.PushAsync<IngredientDetailViewModel, Guid>(id);
        }

        private void NavigateToAdd()
        {
            navigationService.PushAsync<IngredientEditViewModel, Guid?>(null);
        }
    }
}