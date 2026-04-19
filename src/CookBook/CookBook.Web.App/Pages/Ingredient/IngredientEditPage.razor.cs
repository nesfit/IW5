using CookBook.Web.BL.Facades;
using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public partial class IngredientEditPage
    {
        [Inject]
        private NavigationManager navigationManager { get; set; } = null!;

        [Inject]
        private IngredientFacade ingredientFacade { get; set; } = null!;

        [Parameter]
        public Guid Id { get; init; }

        private bool IsLocalIngredient { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            IsLocalIngredient = Id != Guid.Empty && await ingredientFacade.IsLocalAsync(Id);
            await base.OnParametersSetAsync();
        }

        public void NavigateBack()
        {
            navigationManager.NavigateTo("/ingredients");
        }
    }
}
