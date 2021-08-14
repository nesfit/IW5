using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public partial class RecipeEditPage
    {
        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        [Inject]
        private RecipeFacade RecipeFacade { get; set; } = null!;
        [Inject]
        private IngredientFacade IngredientFacade { get; set; } = null!;

        private RecipeDetailModel Data { get; set; } = new();

        [Parameter]
        public Guid Id { get; init; }

        private ICollection<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        private RecipeDetailIngredientModel NewIngredientModel { get; set; } = GetNewIngredientModel();

        private int DurationHours
        {
            get => Data.Duration.Hours;
            set => Data.Duration = new TimeSpan(value, DurationMinutes, 0);
        }

        private int DurationMinutes
        {
            get => Data.Duration.Minutes;
            set => Data.Duration = new TimeSpan(DurationHours, value, 0);
        }

        private string SelectedIngredientName
        {
            get
            {
                return NewIngredientModel.Ingredient.Name;
            }
            set
            {
                NewIngredientModel.Ingredient = Ingredients.First(t => t.Name == value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (Id != Guid.Empty)
            {
                Data = await RecipeFacade.GetByIdAsync(Id);
            }

            Ingredients = await IngredientFacade.GetAllAsync();

            await base.OnInitializedAsync();
        }

        public async Task Save()
        {
            await RecipeFacade.SaveAsync(Data);
            navigationManager.NavigateTo($"/recipes");

        }

        public async Task Delete()
        {
            await RecipeFacade.DeleteAsync(Id);
            navigationManager.NavigateTo($"/recipes");
        }

        public void DeleteIngredient(RecipeDetailIngredientModel ingredient)
        {
            var ingredientIndex = Data.IngredientAmounts.IndexOf(ingredient);
            Data.IngredientAmounts.RemoveAt(ingredientIndex);
        }

        public void AddIngredient()
        {
            Data.IngredientAmounts.Add(NewIngredientModel);
            NewIngredientModel = GetNewIngredientModel();
        }

        private static RecipeDetailIngredientModel GetNewIngredientModel()
        {
            return new(0, Unit.Unknown, new IngredientListModel(Guid.Empty, string.Empty));
        }
    }
}
