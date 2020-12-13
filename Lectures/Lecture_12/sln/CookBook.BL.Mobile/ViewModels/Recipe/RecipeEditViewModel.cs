using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using CookBook.Common.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CookBook.BL.Mobile.ViewModels
{
    public class RecipeEditViewModel : ViewModelBase<Guid?>
    {
        private readonly RecipesFacade recipesFacade;
        private readonly IngredientsFacade ingredientsFacade;
        private readonly INavigationService navigationService;

        public RecipeDetailModel Recipe { get; set; }
        public ObservableCollection<IngredientListModel> IngredientsAll { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public RecipeEditViewModel(Guid? viewModelParameter,
            RecipesFacade recipesFacade,
            IngredientsFacade ingredientsFacade,
            ICommandFactory commandFactory,
            INavigationService navigationService)
            : base(viewModelParameter)
        {
            this.recipesFacade = recipesFacade;
            this.ingredientsFacade = ingredientsFacade;
            this.navigationService = navigationService;

            SaveCommand = commandFactory.CreateCommand(Save);
            CancelCommand = commandFactory.CreateCommand(Cancel);
        }

        public override async Task OnAppearing()
        {
            await base.OnAppearing();

            if (viewModelParameter == null)
            {
                Recipe = new RecipeDetailModel();
            }
            else
            {
                Recipe = await recipesFacade.GetRecipeAsync(viewModelParameter.Value);
            }

            if (IngredientsAll == null)
            {
                IngredientsAll = new ObservableCollection<IngredientListModel>();

                var ingredients = await ingredientsFacade.GetIngredientsAsync();
                IngredientsAll.AddRange(ingredients.OrderBy(ingredient => ingredient.Name));
            }
        }

        private async void Save()
        {
            await recipesFacade.SaveAsync(Recipe);
            await navigationService.PopAsync();
        }

        private async void Cancel()
        {
            await navigationService.PopAsync();
        }
    }
}