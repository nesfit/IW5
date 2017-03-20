using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloWPF.Models;

namespace HelloWPF.ViewModels
{
    public class MenuItemVm
    {
        private readonly MenuItem _menuItem;

        public MenuItemVm(MenuItem menuItem)
        {
            _menuItem = menuItem;
        }

        public string Title => _menuItem.Title;
        public string SubTitle => _menuItem.Title;
    }
}
