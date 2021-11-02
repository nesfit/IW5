using Microsoft.AspNetCore.Components;

namespace CookBook.Web.App.Pages
{
    public class PageBase : ComponentBase
    {
        [Parameter]
        public string Culture { get; set; } = null!;
    }

}
