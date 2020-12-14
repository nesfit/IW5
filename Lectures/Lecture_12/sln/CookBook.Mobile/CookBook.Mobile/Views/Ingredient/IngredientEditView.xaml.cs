using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientEditView
    {
        public IngredientEditView(IngredientEditViewModel ingredientEditViewModel)
            : base(ingredientEditViewModel)
        {
            InitializeComponent();
        }
    }
}