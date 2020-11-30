using CookBook.BL.Web.Facades;
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
        private IngredientsFacade IngredientFacade { get; set; }

        private ICollection<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Ingredients = await IngredientFacade.GetIngredientsAsync();
        }
    }
}