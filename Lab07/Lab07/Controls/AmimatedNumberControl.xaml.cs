using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab07.Controls {
    /// <summary>
    /// Interaction logic for AmimatedNumberControl.xaml
    /// </summary>
    public partial class AmimatedNumberControl : UserControl {

        // Статическое свойство только для чтения DependencyProperty.
        public static readonly DependencyProperty ValueProperty;

        public static RoutedCommand Reset { get; set; }

        static AmimatedNumberControl() {
            // Регистрация свойства
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata {
                CoerceValueCallback = new CoerceValueCallback(CorrectValue),
                BindsTwoWayByDefault = true
            };
            ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(AmimatedNumberControl), metadata,
                new ValidateValueCallback(ValidateValue));

            // Регистрация команды
            Reset = new RoutedCommand("Reset", typeof(MainWindow));
        }

        private static object CorrectValue(DependencyObject dependencyObject, object baseValue) {
            double value = (double)baseValue;
            AmimatedNumberControl amimatedNumberControl = (AmimatedNumberControl)dependencyObject;
            if (amimatedNumberControl.MaxValue != null && value > amimatedNumberControl.MaxValue)  // если > max
                value = (double)amimatedNumberControl.MaxValue;
            if (amimatedNumberControl.MinValue != null && value < amimatedNumberControl.MinValue)  // если < min
                value = (double)amimatedNumberControl.MinValue;
            if (amimatedNumberControl.firstSetValue) {
                amimatedNumberControl.firstSetValue = false;
                amimatedNumberControl.defaultValue = value;
            }
            return value; // иначе возвращаем текущее значение
        }

        private static bool ValidateValue(object value) {
            double currentValue = (double)value;
            if (currentValue >= -0.000001) {// если текущее значение от нуля и выше
                return true;
            }
            return false;
        }

        public AmimatedNumberControl() {
            InitializeComponent();
            this.DataContext = this;
        }

        // Оболочка CLR, реализованная унаследованными методами GetValue()/SetValue().
        public double Value {
            get { return (double)base.GetValue(ValueProperty); }
            set { base.SetValue(ValueProperty, value); }
        }

        public double? MinValue { get; set; }

        public double? MaxValue { get; set; }

        public string Title { get; set; }

        public string Legend {
            get {
                string result = "Значение";
                if (MinValue != null) {
                    result = result + " от " + MinValue;
                }
                if (MaxValue != null) {
                    result = result + " до " + MaxValue;
                }
                return result;
            }
        }

        private void Button_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Debug.WriteLine("TUNNELING event: " + e.RoutedEvent.Name + "\n\tsender: " + sender.ToString() + "\n\tsource: " + e.Source.ToString() + "\n\toriginalsource: " + e.OriginalSource);
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            Debug.WriteLine("DIRECT event: " + e.RoutedEvent.Name + "\n\tsender: " + sender.ToString() + "\n\tsource: " + e.Source.ToString() + "\n\toriginalsource: " + e.OriginalSource);
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            Debug.WriteLine("BUBBLING event: " + e.RoutedEvent.Name + "\n\tsender: " + sender.ToString() + "\n\tsource: " + e.Source.ToString() + "\n\toriginalsource: " + e.OriginalSource);
        }

        double? defaultValue = null;
        bool firstSetValue = true;
        private void Reset_Executed(object sender, ExecutedRoutedEventArgs e) {
            if (defaultValue != null) {
                Value = (double)defaultValue;
            }
        }
    }
}
