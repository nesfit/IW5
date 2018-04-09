using Contacts.MVVM.Framework.Properties;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Contacts.MVVM.Framework
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool isLoaded = false;
        
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsLoaded
        {
            get => isLoaded;
            private set
            {
                if (value == isLoaded) return;
                isLoaded = value;
                OnPropertyChanged();
            }
        }

        public object NavigationParameter { get; set; }

        internal async void StartLoadData()
        {
            await LoadData();
            IsLoaded = true;
        }

        protected virtual Task LoadData()
        {
            return Task.CompletedTask;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}