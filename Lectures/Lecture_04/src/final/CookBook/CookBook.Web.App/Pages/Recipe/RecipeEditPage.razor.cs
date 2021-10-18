using System.Net.Http.Json;
using CookBook.Common.Enums;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public partial class RecipeEditPage
    {
        [Inject]
        private HttpClient httpClient { get; set; } = null!;

        private RecipeDetailModel Data { get; set; } = new();

        [Parameter]
        public Guid Id { get; init; }

        private IList<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        private RecipeDetailIngredientModel NewIngredientModel { get; set; } = new(null, 0, Unit.Unknown, new IngredientListModel(Guid.Empty, string.Empty));

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

        protected override async Task OnInitializedAsync()
        {
            if (Id == Guid.Empty)
            {
                Data = new RecipeDetailModel
                {
                    IngredientAmounts = new List<RecipeDetailIngredientModel>()
                };
            }
            else
            {
                var dataList = await httpClient.GetFromJsonAsync<List<RecipeDetailModel>>("sample-data/recipes.json");
                if (dataList is not null)
                {
                    Data = dataList.Single(item => item.Id == Id);
                }
            }

            Ingredients = await httpClient.GetFromJsonAsync<List<IngredientListModel>>("sample-data/ingredients.json")
                ?? new List<IngredientListModel>();

            await base.OnInitializedAsync();
        }
    }
}
