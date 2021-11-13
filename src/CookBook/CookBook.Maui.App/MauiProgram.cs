using CookBook.Maui.App.Views;
using CookBook.Maui.BL.Installers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace CookBook.Maui.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });


            new MauiBLInstaller().Install(builder.Services, "https://app-iw5-api.azurewebsites.net/");

            builder.Services.AddSingleton<IngredientListView>();

            return builder.Build();
        }
    }
}
