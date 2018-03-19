using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HelloWPF.Annotations;
using HelloWPF.Models;

namespace HelloWPF.ViewModels
{
    public class MenuVm:INotifyPropertyChanged
    {
        private MenuItemVm _selectedItem;
        public ObservableCollection<MenuItemVm> MenuItems { get; } = new ObservableCollection<MenuItemVm>();

        public string SelectedTitle { get; set; }

        public MenuItemVm SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value; 
                this.OnPropertyChanged();
            }
        }

        public MenuVm()
        {
            InitializeFakeMenuItems();
        }

        private void InitializeFakeMenuItems()
        {
            this.MenuItems.Add(new MenuItemVm(new MenuItem() { Title = "abcd", SubTitle = "Lorem ipsum dolores sit amet" }));
            this.MenuItems.Add(new MenuItemVm(new MenuItem() { Title = "abcd1", SubTitle = "Lorem ipsum dolores sit amet" }));
            this.MenuItems.Add(new MenuItemVm(new MenuItem() { Title = "abcd2", SubTitle = "Lorem ipsum dolores sit amet" }));
            this.MenuItems.Add(new MenuItemVm(new MenuItem() { Title = "abcd3", SubTitle = "Lorem ipsum dolores sit amet" }));
            this.MenuItems.Add(new MenuItemVm(new MenuItem() { Title = "abcd4", SubTitle = "Lorem ipsum dolores sit amet" }));
            this.MenuItems.Add(new MenuItemVm(new MenuItem() { Title = "abcd5", SubTitle = "Lorem ipsum dolores sit amet" }));
            this.MenuItems.Add(new MenuItemVm(new MenuItem() { Title = "abcd6", SubTitle = "Lorem ipsum dolores sit amet" }));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}