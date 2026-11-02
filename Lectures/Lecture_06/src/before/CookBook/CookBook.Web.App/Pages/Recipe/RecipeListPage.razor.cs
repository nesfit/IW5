using System.Collections.Generic;
using System.Threading.Tasks;
using CookBook.Common.Models;
using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public partial class RecipeListPage
    {
        [Inject]
        private RecipeFacade RecipeFacade { get; set; } = null!;

        private ICollection<RecipeListModel> Recipes { get; set; } = new List<RecipeListModel>();

        protected override async Task OnInitializedAsync()
        {
            Recipes = await RecipeFacade.GetAllAsync();

            await base.OnInitializedAsync();
        }
    }
}
