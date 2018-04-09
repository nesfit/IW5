using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Contacts.MVVM.Framework
{
    public class WindowCloseBehavior : Behavior<Window>
    {
        public static readonly DependencyProperty CloseButtonProperty = DependencyProperty.Register(nameof(CloseButton), typeof(Button), typeof(WindowCloseBehavior), new FrameworkPropertyMetadata(null, OnButtonChanged));

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(WindowCloseBehavior));

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(WindowCloseBehavior));

        public Button CloseButton
        {
            get => (Button)GetValue(CloseButtonProperty);
            set => SetValue(CloseButtonProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        private static void OnButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = (Window)((WindowCloseBehavior)d).AssociatedObject;
            ((Button)e.NewValue).Click += (s, e1) =>
            {
                var command = ((WindowCloseBehavior)d).Command;
                var commandParameter = ((WindowCloseBehavior)d).CommandParameter;
                command?.Execute(commandParameter);
                window.Close();
            };
        }
    }
}