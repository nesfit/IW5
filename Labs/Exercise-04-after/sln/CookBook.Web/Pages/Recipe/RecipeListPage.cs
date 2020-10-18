using CookBook.BL.Api.Models.Recipe;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookBook.Web.Pages
{
    public partial class RecipeListPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private IList<RecipeListModel> Recipes { get; set; } = new List<RecipeListModel>();

        protected override async Task OnInitializedAsync()
        {
            var response = await httpClient.GetAsync("sample-data/recipes.json");
            var responseText = await response.Content.ReadAsStringAsync();
            Recipes = JsonConvert.DeserializeObject<List<RecipeListModel>>(responseText);

            await base.OnInitializedAsync();
        }
    }
}