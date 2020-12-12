using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeDetailView
    {
        public RecipeDetailView(RecipeDetailViewModel recipeDetailViewModel)
            : base(recipeDetailViewModel)
        {
            InitializeComponent();
        }
    }
}