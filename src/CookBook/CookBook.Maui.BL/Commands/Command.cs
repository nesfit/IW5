using System.Windows.Input;

namespace CookBook.Maui.BL.Commands;

public class Command : ICommand
{
    private readonly Action execute;
    private readonly Func<bool> canExecute;

    public Command(Action execute, Func<bool>? canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute ?? (() => true);
    }

    public bool CanExecute(object parameter)
        => canExecute.Invoke();

    public void Execute(object parameter)
        => execute.Invoke();

    public event EventHandler CanExecuteChanged = null!;
}

internal class Command<T> : ICommand
{
    private readonly Action<T> execute;
    private readonly Func<T, bool> canExecute;

    public Command(Action<T> execute, Func<T, bool>? canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute ?? (T => true);
    }

    public bool CanExecute(object parameter)
        => (parameter is T typedParameter) && canExecute.Invoke(typedParameter);

    public void Execute(object parameter)
    {
        if (parameter is T typedParameter)
        {
            execute.Invoke(typedParameter);
        }
    }

    public event EventHandler CanExecuteChanged = null!;
}
