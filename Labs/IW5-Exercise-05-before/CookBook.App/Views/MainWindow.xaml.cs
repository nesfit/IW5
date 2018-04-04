using System.Windows;
using CookBook.App.ViewModels;
using CookBook.BL;

namespace CookBook.App.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel(new Messenger());
        }
    }
}
