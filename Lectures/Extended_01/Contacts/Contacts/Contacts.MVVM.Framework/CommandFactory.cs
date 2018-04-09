using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Contacts.MVVM.Framework
{
    internal class CommandFactory : ICommandFactory
    {
        public ICommand CreateCommand<T>(Action<T> execute, Predicate<T> canExecute = null, params INotifyPropertyChanged[] viewmodels)
        {
            if (execute == null)
            {
                throw new ArgumentNullException(nameof(execute), $"Parameter {nameof(execute)} can't be null");
            }
            canExecute = canExecute ?? (_ => true);
            return new Command<T>(execute, canExecute, viewmodels);
        }

        public ICommand CreateCommand(Action execute, Func<bool> canExecute = null, params INotifyPropertyChanged[] viewmodels)
        {
            canExecute = canExecute ?? (() => true);
            return CreateCommand<object>(_ => execute(), _ => canExecute(), viewmodels);
        }

        private class Command<T> : ICommand
        {
            private readonly Predicate<T> canExecute;
            private readonly Action<T> execute;

            public event EventHandler CanExecuteChanged;

            public Command(Action<T> execute, Predicate<T> canExecute, INotifyPropertyChanged[] viewmodels)
            {
                this.execute = execute;
                this.canExecute = canExecute;

                foreach (var viewmodel in viewmodels)
                {
                    viewmodel.PropertyChanged += (sender, args) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            }

            public bool CanExecute(T parameter)
            {
                return canExecute(parameter);
            }

            public void Execute(T parameter)
            {
                execute(parameter);
            }

            bool ICommand.CanExecute(object parameter)
            {
                return CanExecute((T)parameter);
            }

            void ICommand.Execute(object parameter)
            {
                Execute((T)parameter);
            }
        }
    }
}