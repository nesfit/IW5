using CookBook.Maui.BL.ViewModels;

namespace CookBook.Maui.App.Views
{
    public partial class IngredientListView
    {
        private readonly IngredientListViewModel ingredientListViewModel;

        public IngredientListView(IngredientListViewModel ingredientListViewModel)
        {
            this.ingredientListViewModel = ingredientListViewModel;

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
