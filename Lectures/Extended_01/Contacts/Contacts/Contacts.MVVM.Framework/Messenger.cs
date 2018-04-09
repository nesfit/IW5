using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace Contacts.MVVM.Framework
{
    internal class Messenger : IMessenger
    {
        private readonly ConcurrentDictionary<Type, ConcurrentBag<Delegate>> registeredActions = new ConcurrentDictionary<Type, ConcurrentBag<Delegate>>();

        public void Register<TMessage>(Action<TMessage> action) where TMessage : class
        {
            Register<TMessage>(actionDelegate: action);
        }

        public void Register<TMessage>(Func<TMessage, Task> asyncAction) where TMessage : class
        {
            Register<TMessage>(actionDelegate: asyncAction);
        }

        public async Task Send<TMessage>(TMessage message) where TMessage : class
        {
            ConcurrentBag<Delegate> actions;
            if (registeredActions.TryGetValue(typeof(TMessage), out actions))
            {
                foreach (var action in actions.Select(a => a as Action<TMessage>).Where(a => a != null))
                {
                    action(message);
                }
                foreach (var action in actions.Select(a => a as Func<TMessage, Task>).Where(a => a != null))
                {
                    await action(message);
                }
            }
        }

        public void UnRegister<TMessage>(Action<TMessage> action) where TMessage : class
        {
            UnRegister<TMessage>(actionDelegate: action);
        }

        public void UnRegister<TMessage>(Func<TMessage, Task> asyncAction) where TMessage : class
        {
            UnRegister<TMessage>(actionDelegate: asyncAction);
        }

        private void Register<TMessage>(Delegate actionDelegate) where TMessage : class
        {
            var key = typeof(TMessage);

            ConcurrentBag<Delegate> actions;
            if (!registeredActions.TryGetValue(typeof(TMessage), out actions))
            {
                actions = new ConcurrentBag<Delegate>();
                registeredActions[key] = actions;
            }
            actions.Add(actionDelegate);
        }

        private void UnRegister<TMessage>(Delegate actionDelegate) where TMessage : class
        {
            var key = typeof(TMessage);

            ConcurrentBag<Delegate> actions;
            if (registeredActions.TryGetValue(typeof(TMessage), out actions))
            {
                var actionsList = actions.ToList();
                actionsList.Remove(actionDelegate);
                registeredActions[key] = new ConcurrentBag<Delegate>(actionsList);
            }
        }
    }
}