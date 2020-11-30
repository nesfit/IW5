using CookBook.BL.Web.Facades;
using CookBook.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CookBook.Web.Pages
{
    public partial class RecipeEditPage
    {

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private RecipesFacade RecipeFacade { get; set; } 
        [Inject]
        private IngredientsFacade IngredientFacade { get; set; } 

        private RecipeDetailModel Data { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        public ICollection<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        public RecipeDetailIngredientModel NewIngredientModel { get; set; } = new RecipeDetailIngredientModel();

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
                return NewIngredientModel.Ingredient.Name;
            }
            set
            {
                NewIngredientModel.Ingredient = Ingredients.SingleOrDefault(t => t.Name == value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            if (Id == Guid.Empty)
            {
                Data = new RecipeDetailModel
                {
                    Ingredients = new List<RecipeDetailIngredientModel>()
                };
            }
            else
            {
                Data = await RecipeFacade.GetRecipesAsync(Id);
            }

            Ingredients = await IngredientFacade.GetIngredientsAsync();

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
            var ingredientIndex = Data.Ingredients.IndexOf(ingredient);
            Data.Ingredients.RemoveAt(ingredientIndex);
        }

        public void AddIngredient()
        {
            Data.Ingredients.Add(NewIngredientModel);
            NewIngredientModel = new RecipeDetailIngredientModel();
        }
    }
}