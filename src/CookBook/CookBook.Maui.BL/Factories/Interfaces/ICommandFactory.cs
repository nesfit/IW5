using System.Linq.Expressions;
using System.Windows.Input;
using CookBook.Maui.BL.Commands;

namespace CookBook.Maui.BL.Factories;

public interface ICommandFactory
{
    ICommand CreateCommand(Action execute, Expression<Func<bool>>? canExecute = null);
    ICommand CreateCommand<T>(Action<T> execute, Expression<Func<T, bool>>? canExecute = null);
    
    IAsyncCommand CreateCommand(Func<Task> execute, Func<bool>? canExecute = null);
    IAsyncCommand<T> CreateCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null);
}
