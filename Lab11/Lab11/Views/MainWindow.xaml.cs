using Lab11.ViewModels;
using System.Windows;

namespace Lab11 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private MainWindowViewModel vm;
        public MainWindow() {
            InitializeComponent();

            vm = new MainWindowViewModel();
            DataContext = vm;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            vm.Dispose();
        }
    }
}
