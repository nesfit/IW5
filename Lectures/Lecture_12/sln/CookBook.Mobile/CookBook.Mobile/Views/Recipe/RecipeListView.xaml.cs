using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeListView
    {
        public RecipeListView(RecipeListViewModel recipeListViewModel)
            : base(recipeListViewModel)
        {
            InitializeComponent();
        }
    }
}