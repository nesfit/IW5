using CookBook.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CookBook.Web.Pages
{
    public partial class IngredientListPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private IList<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        protected override async Task OnInitializedAsync()
        {
            Ingredients = await httpClient.GetFromJsonAsync<List<IngredientListModel>>("sample-data/ingredients.json");

            await base.OnInitializedAsync();
        }
    }
}