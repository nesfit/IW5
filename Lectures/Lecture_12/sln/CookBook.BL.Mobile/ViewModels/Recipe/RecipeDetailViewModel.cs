using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookBook.BL.Mobile.ViewModels
{
    public class RecipeDetailViewModel : ViewModelBase<Guid>
    {
        private readonly RecipesFacade recipesFacade;
        private readonly ICommandFactory commandFactory;
        private readonly INavigationService navigationService;

        public RecipeDetailModel Recipe { get; set; } = new RecipeDetailModel();
        public ICommand DeleteCommand { get; set; }
        public ICommand NavigateToEditCommand { get; set; }

        public RecipeDetailViewModel(Guid viewModelParameter,
            RecipesFacade recipesFacade,
            ICommandFactory commandFactory,
            INavigationService navigationService)
            : base(viewModelParameter)
        {
            this.recipesFacade = recipesFacade;
            this.commandFactory = commandFactory;
            this.navigationService = navigationService;

            DeleteCommand = commandFactory.CreateCommand(Delete);
            NavigateToEditCommand = commandFactory.CreateCommand(NavigateToEdit);
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            Recipe = await recipesFacade.GetRecipeAsync(viewModelParameter);
        }

        private async void Delete()
        {
            await recipesFacade.DeleteAsync(Recipe.Id);
            await navigationService.PopAsync();
        }

        private async void NavigateToEdit()
        {
            await navigationService.PushAsync<RecipeEditViewModel, Guid?>(Recipe.Id);
        }
    }
}