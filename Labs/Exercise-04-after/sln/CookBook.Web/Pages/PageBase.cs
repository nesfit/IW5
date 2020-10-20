using Microsoft.AspNetCore.Components;

namespace CookBook.Web.Pages
{
    public class PageBase : ComponentBase
    {
        [Parameter]
        public string Culture { get; set; }
    }
}