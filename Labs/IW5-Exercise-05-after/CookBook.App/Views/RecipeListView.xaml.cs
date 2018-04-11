using System.Windows.Controls;
using CookBook.App.ViewModels;

namespace CookBook.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RecipeListView : UserControl
    {
        public RecipeListView()
        {
            InitializeComponent();
            Loaded += RecipeListView_Loaded;
        }

        private void RecipeListView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is RecipeListViewModel viewModel)
            {
                viewModel.OnLoad();
            }
        }
    }
}