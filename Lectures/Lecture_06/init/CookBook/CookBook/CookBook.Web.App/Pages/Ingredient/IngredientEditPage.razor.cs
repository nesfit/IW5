
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public partial class IngredientEditPage
    {
        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        [Parameter]
        public Guid Id { get; set; }

        public void NavigateBack()
        {
            navigationManager.NavigateTo("/ingredients");
        }
    }
}
