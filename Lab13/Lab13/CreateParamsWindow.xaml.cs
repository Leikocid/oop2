using Lab13.Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Shapes;

namespace Lab13 {
    /// <summary>
    /// Interaction logic for CreateParamsWindow.xaml
    /// </summary>
    public partial class CreateParamsWindow : Window {
        public CreateParamsWindow() {
            InitializeComponent();
        }


        public List<Shape> result = new List<Shape>();


        private void Button_Click(object sender, RoutedEventArgs e) {
            AbstractShapeFactory factory;
            if (btnWhite.IsChecked == true) {
                factory = new WhiteFactory();
            } else {
                factory = new CoralFactory();
            }

            for (int i = 0; i < sldCount.Value; i++) {
                if (btnRectangle.IsChecked == true) {
                    result.Add(factory.CreateRectangle());
                } else {
                    result.Add(factory.CreateEllipse());
                }
            }

            Close();
        }
    }
}
