using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using CookBook.Common.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookBook.BL.Mobile.ViewModels
{
    public class RecipeListViewModel : ViewModelBase
    {
        private readonly RecipesFacade recipesFacade;
        private readonly INavigationService navigationService;

        public ObservableCollection<RecipeListModel> Recipes { get; set; } = new ObservableCollection<RecipeListModel>();
        public ICommand NavigateToDetailCommand { get; set; }

        public RecipeListViewModel(RecipesFacade recipesFacade,
            ICommandFactory commandFactory,
            INavigationService navigationService)
        {
            this.recipesFacade = recipesFacade;
            this.navigationService = navigationService;

            NavigateToDetailCommand = commandFactory.CreateCommand<Guid>(NavigateToDetail);
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            try
            {
                var recipes = await recipesFacade.GetRecipesAsync();

                Recipes.Clear();
                Recipes.AddRange(recipes);
            }
            catch (ApiException)
            {
            }
        }

        private void NavigateToDetail(Guid id)
        {
            navigationService.PushAsync<RecipeDetailViewModel, Guid>(id);
        }
    }
}