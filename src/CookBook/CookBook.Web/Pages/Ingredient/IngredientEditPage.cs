using System;
using Microsoft.AspNetCore.Components;

namespace CookBook.App.Web.Pages
{
    public partial class IngredientEditPage
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        public void NavigateBack()
        {
            navigationManager.NavigateTo($"/ingredients");
        }
    }
}