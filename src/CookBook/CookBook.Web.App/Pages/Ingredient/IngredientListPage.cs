using System.Collections.Generic;
using System.Threading.Tasks;
using CookBook.Common.Models;
using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public partial class IngredientListPage
    {
        [Inject]
        private IngredientFacade IngredientFacade { get; set; } = null!;

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
