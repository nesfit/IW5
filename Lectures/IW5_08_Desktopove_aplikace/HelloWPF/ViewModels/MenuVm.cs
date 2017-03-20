using System.Collections.ObjectModel;
using HelloWPF.Models;

namespace HelloWPF.ViewModels
{
    public class MenuVm
    {
        public ObservableCollection<MenuItemVm> MenuItems { get; } = new ObservableCollection<MenuItemVm>();

        public string SelectedTitle { get; set; }

        public MenuItemVm SelectedItem { get; set; }

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
        }
    }
}