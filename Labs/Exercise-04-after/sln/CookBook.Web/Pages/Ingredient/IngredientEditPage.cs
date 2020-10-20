using Microsoft.AspNetCore.Components;
using System;

namespace CookBook.Web.Pages
{
    public partial class IngredientEditPage
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        public void NavigateBack()
        {
            navigationManager.NavigateTo("/ingredients");
        }
    }
}