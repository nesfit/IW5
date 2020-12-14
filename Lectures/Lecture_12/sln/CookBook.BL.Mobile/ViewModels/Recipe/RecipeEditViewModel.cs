using CookBook.BL.Mobile.Api;
using CookBook.BL.Mobile.Facades;
using CookBook.BL.Mobile.Factories;
using CookBook.BL.Mobile.Services;
using System;
using System.Collections.Generic;
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
        public RecipeDetailIngredientModel RecipeIngredientModelNew { get; set; } = new RecipeDetailIngredientModel();
        public ObservableCollection<IngredientListModel> IngredientsAll { get; set; }
        public List<string> UnitTexts { get; set; }
        public List<string> FoodTypeTexts { get; set; }
        public ICommand AddIngredientCommand { get; set; }
        public ICommand RemoveIngredientCommand { get; set; }
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

            AddIngredientCommand = commandFactory.CreateCommand(AddIngredient);
            RemoveIngredientCommand = commandFactory.CreateCommand<Guid>(RemoveIngredient);
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
                OnPropertyChanged("Recipe.FoodType");
            }

            if (IngredientsAll == null)
            {
                IngredientsAll = await ingredientsFacade.GetIngredientsAsync();
            }

            if (UnitTexts == null)
            {
                UnitTexts = Enum.GetNames(typeof(Unit)).ToList();
            }

            if (FoodTypeTexts == null)
            {
                FoodTypeTexts = Enum.GetNames(typeof(FoodType)).ToList();
            }
        }

        private void AddIngredient()
        {
            if (RecipeIngredientModelNew.Ingredient != null
                && RecipeIngredientModelNew.Amount > 0
                && RecipeIngredientModelNew.Unit != Unit.Unknown
                && !Recipe.Ingredients.Any(recipeDetailIngredientModel =>
                    recipeDetailIngredientModel.Ingredient.Id == RecipeIngredientModelNew.Ingredient.Id))
            {
                Recipe.Ingredients.Add(RecipeIngredientModelNew);
                RecipeIngredientModelNew = new RecipeDetailIngredientModel();
            }
        }

        private void RemoveIngredient(Guid ingredientId)
        {
            var recipeDetailIngredientModel = Recipe.Ingredients.FirstOrDefault(ingredient => ingredient.Ingredient.Id == ingredientId);
            Recipe.Ingredients.Remove(recipeDetailIngredientModel);
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