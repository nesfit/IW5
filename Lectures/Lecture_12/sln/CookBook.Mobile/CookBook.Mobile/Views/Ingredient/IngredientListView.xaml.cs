using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientListView
    {
        public IngredientListView(IngredientListViewModel ingredientListViewModel)
            : base(ingredientListViewModel)
        {
            InitializeComponent();
        }
    }
}