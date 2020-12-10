using Microsoft.AspNetCore.Components;

namespace CookBook.Web.Blazor.Pages
{
    public class PageBase : ComponentBase
    {
        [Parameter]
        public string Culture { get; set; }
    }
}