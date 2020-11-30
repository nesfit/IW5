using CookBook.BL.Mobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace CookBook.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngredientsListView
    {
        public IngredientsListView(IngredientsListViewModel ingredientsListViewModel)
            : base(ingredientsListViewModel)
        {
            InitializeComponent();
        }
    }
}