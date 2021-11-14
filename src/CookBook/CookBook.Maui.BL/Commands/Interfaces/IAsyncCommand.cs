using System.Windows.Input;

namespace CookBook.Maui.BL.Commands;

public interface IAsyncCommand : ICommand
{
    Task ExecuteAsync();
}

public interface IAsyncCommand<T> : ICommand
{
    Task ExecuteAsync(T parameter);
}
