using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AdvancedConstructsInCsharp.Events
{
    public class Customer : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;

        public event PropertyChangedEventHandler PropertyChanged;

        public string FirstName
        {
            get { return this._firstName; }
            set
            {
                this._firstName = value;
                this.OnPropertyChanged(); //Property name can be omitted  with [CallerMemberName] attribute
                this.OnPropertyChanged("FullName");
            }
        }

        public string LastName
        {
            get { return this._lastName; }
            set
            {
                this._lastName = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged("FullName");
            }
        }

        public string FullName
        {
            get { return this.FirstName + " " + this.LastName; }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}