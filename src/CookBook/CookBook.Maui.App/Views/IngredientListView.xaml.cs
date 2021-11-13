using CookBook.Maui.BL.ViewModels;

namespace CookBook.Maui.App.Views
{
    public partial class IngredientListView
    {
        public IngredientListView(IngredientListViewModel ingredientListViewModel)
        {
            InitializeComponent();
            BindingContext = ingredientListViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await (BindingContext as IngredientListViewModel)?.OnAppearingAsync();
        }
    }
}
