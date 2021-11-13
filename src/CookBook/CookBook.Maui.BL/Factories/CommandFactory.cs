using System.Linq.Expressions;
using System.Windows.Input;
using CookBook.Maui.BL.Commands;

namespace CookBook.Maui.BL.Factories;

public class CommandFactory : ICommandFactory
{
    public ICommand CreateCommand(Action execute, Expression<Func<bool>>? canExecute)
    {
        return new Command(execute, canExecute?.Compile());
    }

    public ICommand CreateCommand<T>(Action<T> execute, Expression<Func<T, bool>>? canExecute)
    {
        return new Command<T>(execute, canExecute?.Compile());
    }

    public IAsyncCommand CreateCommand(Func<Task> execute, Func<bool>? canExecute)
    {
        return new AsyncCommand(execute, canExecute);
    }

    public IAsyncCommand<T> CreateCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute)
    {
        return new AsyncCommand<T>(execute, canExecute);
    }
}
