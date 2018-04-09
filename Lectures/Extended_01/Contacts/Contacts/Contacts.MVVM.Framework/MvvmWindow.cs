using System.Windows;

namespace Contacts.MVVM.Framework
{
    public class MvvmWindow : Window
    {
        public object NavigationParameter { get; set; }

        public MvvmWindow()
        {
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewmodel = DataContext as ViewModelBase;
            if (viewmodel != null)
            {
                viewmodel.NavigationParameter = this.NavigationParameter;
                viewmodel.StartLoadData();
            }
        }
    }
}