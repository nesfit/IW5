using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HelloWPF
{
    public class BindingSampleViewModel : INotifyPropertyChanged
    {
        private string _name;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Name {
            get => this._name;
            set {
                this._name = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.FancyName));
            }
        }

        public string FancyName => $"***this.Name***";

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}