namespace CookBook.Maui.BL.Commands;

public class AsyncCommand : IAsyncCommand
{
    private readonly Func<Task> execute;
    private readonly Func<bool> canExecute;

    public AsyncCommand(
        Func<Task> execute,
        Func<bool>? canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute ?? (() => true);
    }

    public bool CanExecute(object parameter)
        => canExecute.Invoke();

    public void Execute(object parameter)
        => ExecuteAsync();

    public event EventHandler? CanExecuteChanged;

    public Task ExecuteAsync()
        => execute.Invoke();
}

public class AsyncCommand<T> : IAsyncCommand<T>
{
    private readonly Func<T, Task> execute;
    private readonly Func<T, bool> canExecute;

    public AsyncCommand(
        Func<T, Task> execute,
        Func<T, bool>? canExecute)
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
            ExecuteAsync(typedParameter);
        }
    }

    public event EventHandler? CanExecuteChanged;

    public Task ExecuteAsync(T parameter)
        => execute.Invoke(parameter);
}
