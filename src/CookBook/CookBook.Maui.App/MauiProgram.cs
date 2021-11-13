using CookBook.Maui.App.Installers;
using CookBook.Maui.App.Services;
using CookBook.Maui.BL.Installers;
using CookBook.Maui.BL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace CookBook.Maui.App;

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

        var navigationPage = new NavigationPage();

        new MauiBLInstaller().Install(builder.Services, "https://app-iw5-api.azurewebsites.net/");
        new MauiAppInstaller().Install(builder.Services, navigationPage);

        var dependencyInjectionService = new DependencyInjectionService();
        builder.Services.AddSingleton<IDependencyInjectionService>(dependencyInjectionService);

        return dependencyInjectionService.Build(builder);
    }
}
