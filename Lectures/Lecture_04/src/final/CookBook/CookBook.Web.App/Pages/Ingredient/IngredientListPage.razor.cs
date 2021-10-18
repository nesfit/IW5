using System.Net.Http.Json;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public partial class IngredientListPage
    {
        [Inject]
        private HttpClient httpClient { get; set; } = null!;

        private IList<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        protected override async Task OnInitializedAsync()
        {
            Ingredients = await httpClient.GetFromJsonAsync<List<IngredientListModel>>("sample-data/ingredients.json")
                          ?? new List<IngredientListModel>();

            await base.OnInitializedAsync();
        }
    }
}
