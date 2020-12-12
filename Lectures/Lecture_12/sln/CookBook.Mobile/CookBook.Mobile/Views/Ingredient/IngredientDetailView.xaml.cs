using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientDetailView
    {
        public IngredientDetailView(IngredientDetailViewModel ingredientDetailViewModel)
            : base(ingredientDetailViewModel)
        {
            InitializeComponent();
        }
    }
}