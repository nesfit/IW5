using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecipeEditView
    {
        public RecipeEditView(RecipeEditViewModel recipeEditViewModel)
            : base(recipeEditViewModel)
        {
            InitializeComponent();
        }
    }
}