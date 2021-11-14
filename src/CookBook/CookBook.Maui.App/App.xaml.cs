using CookBook.Maui.App.Services;
using CookBook.Maui.BL.ViewModels;
using Microsoft.Maui.Controls;

namespace CookBook.Maui.App
{
    public partial class App : Application
    {
        public App(NavigationService navigationService)
        {
            InitializeComponent();

            var _ = navigationService.PushAsync<IngredientListViewModel>();
            MainPage = navigationService.RootNavigationPage;
        }
    }
}
