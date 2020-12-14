using CookBook.BL.Web.Facades;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CookBook.Models;

namespace CookBook.Web.Pages
{
    public partial class RecipeListPage
    {
        [Inject]
        private RecipesFacade RecipeFacade { get; set; }

        private ICollection<RecipeListModel> Recipes { get; set; } = new List<RecipeListModel>();

        protected override async Task OnInitializedAsync()
        {   
            Recipes = await RecipeFacade.GetRecipesAsync();

            await base.OnInitializedAsync();
        }
    }
}