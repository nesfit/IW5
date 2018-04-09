using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Contacts.MVVM.Framework
{
    public interface ICommandFactory
    {
        ICommand CreateCommand<T>(Action<T> execute, Predicate<T> canExecute = null, params INotifyPropertyChanged[] viewmodels);

        ICommand CreateCommand(Action execute, Func<bool> canExecute = null, params INotifyPropertyChanged[] viewmodels);
    }
}