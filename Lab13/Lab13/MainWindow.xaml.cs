using Lab13.Model;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab13 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        internal static FlowDocument log;
        Random random = new Random();

        public MainWindow() {
            InitializeComponent();

            log = fdLog;
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            CreateParamsWindow dialog = new CreateParamsWindow();
            dialog.ShowDialog();

            dialog.result.ForEach(s => {
                s.SetValue(Canvas.LeftProperty, random.NextDouble() * (canvas.ActualWidth - 50));
                s.SetValue(Canvas.TopProperty, random.NextDouble() * (canvas.ActualHeight - 50));
                canvas.Children.Add(s);
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            Shape s = StarSingletonFactory.GetStar();
            s.SetValue(Canvas.LeftProperty, random.NextDouble() * (canvas.ActualWidth - 50));
            s.SetValue(Canvas.TopProperty, random.NextDouble() * (canvas.ActualHeight - 50));
            canvas.Children.Add(s);
        }

        private void BtnBuild_Click(object sender, RoutedEventArgs e) {
            RandomFactory drankDirector = new RandomFactory();
            for (int i = 0; i < 5; i++) {
                Shape s = drankDirector.BuildRandomShape(); // директор 
                double left = random.NextDouble() * (canvas.ActualWidth - 50);
                double top = random.NextDouble() * (canvas.ActualHeight - 50);
                s.SetValue(Canvas.LeftProperty, left);
                s.SetValue(Canvas.TopProperty, top);
                canvas.Children.Add(s);

                // Добавление копий
                if (s is TreeShape) {
                    TreeShape tree = s as TreeShape;
                    tree.Stroke = Brushes.Blue;
                    for (int j = 0; j < 2; j++) {
                        left += tree.Width / 2;
                        top += tree.Height * 0.2;

                        TreeShape copy = tree.Clone() as TreeShape; // копия
                        copy.SetValue(Canvas.LeftProperty, left);
                        copy.SetValue(Canvas.TopProperty, top);
                        copy.Height = copy.Height * 0.8;
                        copy.Width = copy.Width * 0.8;
                        copy.Stroke = Brushes.Green;
                        canvas.Children.Add(copy);

                        tree = copy;
                    }
                }
            }
        }
    }
}
