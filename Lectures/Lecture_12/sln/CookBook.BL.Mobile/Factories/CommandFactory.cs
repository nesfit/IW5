using CookBook.BL.Mobile.Commands;
using System;
using System.Linq.Expressions;
using System.Windows.Input;

namespace CookBook.BL.Mobile.Factories
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand(Action execute, Expression<Func<bool>> canExecute = null)
            => new Command(execute, canExecute?.Compile());

        public ICommand CreateCommand<T>(Action<T> execute, Expression<Func<T, bool>> canExecute = null)
            => new Command<T>(execute, canExecute?.Compile());
    }
}