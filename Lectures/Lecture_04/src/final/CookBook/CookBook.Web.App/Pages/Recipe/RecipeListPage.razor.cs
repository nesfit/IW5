using System.Net.Http.Json;
using CookBook.Common.Models;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public partial class RecipeListPage
    {
        [Inject]
        private HttpClient httpClient { get; set; } = null!;

        private IList<RecipeListModel> Recipes { get; set; } = new List<RecipeListModel>();

        protected override async Task OnInitializedAsync()
        {
            Recipes = await httpClient.GetFromJsonAsync<List<RecipeListModel>>("sample-data/recipes.json")
                ?? new List<RecipeListModel>();

            await base.OnInitializedAsync();
        }
    }
}
