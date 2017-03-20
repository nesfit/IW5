namespace HelloWPF.ViewModels
{
    public class MainWindowVm
    {
        public MainWindowVm()
        {
            NavigationMenu = new MenuVm();
        }

        public MenuVm NavigationMenu { get; set; }
    }
}