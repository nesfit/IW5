using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView
    {
        public HomeView(HomeViewModel homeViewModel)
            : base(homeViewModel)
        {
            InitializeComponent();
        }
    }
}