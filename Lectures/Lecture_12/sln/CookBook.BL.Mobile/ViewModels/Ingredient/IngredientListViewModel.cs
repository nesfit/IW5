using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using CookBook.Common.Extensions;
using CookBook.Models;
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

        public IngredientListViewModel(IngredientsFacade ingredientsFacade,
            ICommandFactory commandFactory,
            INavigationService navigationService)
        {
            this.ingredientsFacade = ingredientsFacade;
            this.navigationService = navigationService;
            NavigateToDetailCommand = commandFactory.CreateCommand<Guid>(NavigateToDetail);
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            try
            {
                var ingredients = await ingredientsFacade.GetIngredientsAsync();

                Ingredients.Clear();
                Ingredients.AddRange(ingredients);
            }
            catch (ApiException)
            {
            }
        }

        private async void NavigateToDetail(Guid id)
        {
            await navigationService.PushAsync<IngredientDetailViewModel, Guid>(id);
        }
    }
}