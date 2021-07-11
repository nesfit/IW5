using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookBook.BL.Web.Facades;
using CookBook.Common.Enums;
using CookBook.Models;
using Microsoft.AspNetCore.Components;

namespace CookBook.App.Web.Pages
{
    public partial class RecipeEditPage
    {

        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        [Inject]
        private RecipeFacade RecipeFacade { get; set; } = null!;
        [Inject]
        private IngredientFacade IngredientFacade { get; set; } = null!;

        private RecipeDetailModel Data { get; set; } = new RecipeDetailModel(Guid.NewGuid(), string.Empty, string.Empty, TimeSpan.FromSeconds(0), FoodType.Unknown);

        [Parameter]
        public Guid Id { get; set; }

        public ICollection<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        public RecipeDetailIngredientModel NewIngredientModel { get; set; } = GetNewIngredientModel();

        public int DurationHours
        {
            get => Data.Duration.Hours;
            set => Data.Duration = new TimeSpan(value, DurationMinutes, 0);
        }

        public int DurationMinutes
        {
            get => Data.Duration.Minutes;
            set => Data.Duration = new TimeSpan(DurationHours, value, 0);
        }

        public string SelectedIngredientName
        {
            get
            {
                return NewIngredientModel.Ingredient?.Name ?? string.Empty;
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
            => new(0, Unit.Unknown, new IngredientListModel(Guid.Empty, string.Empty));
    }
}
