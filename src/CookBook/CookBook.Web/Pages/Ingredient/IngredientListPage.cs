using System.Collections.Generic;
using System.Threading.Tasks;
using CookBook.BL.Web.Facades;
using CookBook.Models;
using Microsoft.AspNetCore.Components;

namespace CookBook.App.Web.Pages
{
    public partial class IngredientListPage
    {
        [Inject]
        private IngredientFacade IngredientFacade { get; set; }

        private ICollection<IngredientListModel> Ingredients { get; set; } = new List<IngredientListModel>();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            await base.OnInitializedAsync();
        }

        private async Task LoadData()
        {
            Ingredients = await IngredientFacade.GetAllAsync();
        }
    }
}