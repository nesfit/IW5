using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App
{
    public partial class MainLayout
    {
        [Inject]
        public RecipeFacade RecipeFacade { get; set; } = null!;

        [Inject]
        public IngredientFacade IngredientFacade { get; set; } = null!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = null!;
    }
}
