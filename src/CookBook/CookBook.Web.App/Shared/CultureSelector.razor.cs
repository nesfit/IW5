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

        private string SelectedLanguageName
        {
            get => CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            set
            {
                if (value != CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
                {
                    try
                    {
                        var jsInProcessRuntime = (IJSInProcessRuntime)jsRuntime;
                        jsInProcessRuntime.InvokeVoid("language.set", value);
                        
                        navigationManager.NavigateTo(navigationManager.Uri, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error setting culture: {ex.Message}");
                    }
                }
            }
        }

        private string GetLanguageDisplay(CultureInfo culture)
        {
            var icon = culture.TwoLetterISOLanguageName switch
            {
                "cs" => "🇨🇿",
                "en" => "🇺🇸",
                _ => $"🏳️"
            };

            return $"{icon} {culture.TwoLetterISOLanguageName.ToUpper()}";
        }
    }
}
