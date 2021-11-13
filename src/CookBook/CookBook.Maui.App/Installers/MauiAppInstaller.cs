using CookBook.Maui.App.Services;
using CookBook.Maui.App.Views;
using CookBook.Maui.BL.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace CookBook.Maui.App.Installers
{
    internal class MauiAppInstaller
    {
        public void Install(IServiceCollection serviceCollection, NavigationPage rootNavigationPage)
        {
            serviceCollection.AddSingleton(rootNavigationPage);

            serviceCollection.AddSingleton<NavigationService>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();

            serviceCollection.AddTransient<IngredientListView>();
            serviceCollection.AddTransient<IngredientDetailView>();

        }
    }
}
