using CookBook.Maui.BL.ViewModels;

namespace CookBook.Maui.App.Views
{
    public abstract partial class ContentPageBase
    {
        public IViewModel ViewModel
        {
            set
            {
                BindingContext = value;
            }
        }

        protected ContentPageBase()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IViewModel viewModel)
            {
                await viewModel.OnAppearingAsync();
            }
        }
    }
}
