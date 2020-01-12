using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace IW5_Xamarin
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        public int Operand1 { get; set; }
        public int Operand2 { get; set; }
        public int Result { get; set; }

        public ICommand CalculateCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;


        public MainPageViewModel()
        {
            CalculateCommand = new Command<Operation>(CalculateOperation);
        }

        private void CalculateOperation(Operation operation)
        {
            switch (operation)
            {
                case Operation.Add:
                    Result = Operand1 + Operand2;
                    break;
                case Operation.Substract:
                    Result = Operand1 - Operand2;
                    break;
                case Operation.Multiply:
                    Result = Operand1 * Operand2;
                    break;
                case Operation.Divide:
                    if (Operand2 != 0)
                    {
                        Result = Operand1 / Operand2;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
