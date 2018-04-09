using System;
using System.Threading.Tasks;

namespace Contacts.MVVM.Framework
{
    public interface IMessenger
    {
        void Register<TMessage>(Action<TMessage> action) where TMessage : class;
        void Register<TMessage>(Func<TMessage, Task> asyncAction) where TMessage : class;
        Task Send<TMessage>(TMessage message) where TMessage : class;
        void UnRegister<TMessage>(Action<TMessage> action) where TMessage : class;
        void UnRegister<TMessage>(Func<TMessage, Task> asyncAction) where TMessage : class;
    }
}