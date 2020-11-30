using CookBook.BL.Mobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewBase : ContentPage
    {
        private readonly IViewModel viewModel;

        public ViewBase(IViewModel viewModel)
        {
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel?.OnAppearing();
        }
    }
}