using System.Windows.Input;

namespace Contacts.ViewModels.Tests
{
    public static class TestExtensions
    {
        public static void TryExecute(this ICommand command, object parameter)
        {
            if (command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }
        }
    }
}