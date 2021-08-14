using System.Threading.Tasks;
using CookBook.BL.Web.Facades;
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


        public async Task OnlineStatusChangedAsync(bool isOnline)
        {
            if (isOnline)
            {
                var dataChanged = false;
                dataChanged = dataChanged || await IngredientFacade.SynchronizeLocalDataAsync();
                dataChanged = dataChanged || await RecipeFacade.SynchronizeLocalDataAsync();

                if (dataChanged)
                {
                    NavigationManager.NavigateTo(NavigationManager.Uri, true);
                }
            }
        }
    }
}