using CookBook.BL.Api.Models.Recipe;
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
        private HttpClient httpClient { get; set; }

        private RecipeDetailModel Data { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        public IList<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

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
                var response = await httpClient.GetAsync("sample-data/recipes.json");
                var responseText = await response.Content.ReadAsStringAsync();
                var dataList = JsonConvert.DeserializeObject<List<RecipeDetailModel>>(responseText);
                Data = dataList.Single(item => item.Id == Id);
            }

            Ingredients = await httpClient.GetFromJsonAsync<List<IngredientListModel>>("sample-data/ingredients.json");

            await base.OnInitializedAsync();
        }
    }
}