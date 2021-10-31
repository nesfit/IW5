using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CookBook.Web.App
{
    public partial class CultureSelector
    {
        [Inject]
        private IJSRuntime jsRuntime { get; set; } = null!;

        [Inject]
        private NavigationManager navigationManager {  get; set; } = null!;

        private CultureInfo[] supportedCultures = {
            new("cs"),
            new("en"),
        };

        private CultureInfo Culture
        {
            get => CultureInfo.CurrentCulture;
            set
            {
                if (!Equals(CultureInfo.CurrentCulture, value))
                {
                    var jsInProcessRuntime = (IJSInProcessRuntime)jsRuntime;
                    jsInProcessRuntime.InvokeVoid("blazorCulture.set", value.Name);

                    navigationManager.NavigateTo(navigationManager.Uri, true);
                }
            }
        }
    }
}
