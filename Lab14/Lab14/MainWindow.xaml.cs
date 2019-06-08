using Lab14.Model;
using Lab14.Model.Composite;
using Lab14.Model.Factory;
using Lab14.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab14 {
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

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            AddToCanvas(
                StarSingletonFactory.GetStar(),
                random.NextDouble() * (canvas.ActualWidth - 50),
                random.NextDouble() * (canvas.ActualHeight - 50)
            );
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            CreateParamsWindow dialog = new CreateParamsWindow();
            dialog.ShowDialog();

            dialog.result.ForEach(s => {
                AddToCanvas(
                    s,
                    random.NextDouble() * (canvas.ActualWidth - 50),
                    random.NextDouble() * (canvas.ActualHeight - 50));
            });
        }

        private void BtnBuild_Click(object sender, RoutedEventArgs e) {
            // создаем набор случайных примитивов
            RandomFactory drankDirector = new RandomFactory();
            for (int i = 0; i < 5; i++) {
                AddToCanvas(
                    drankDirector.BuildRandomShape(), // директор 
                    random.NextDouble() * (canvas.ActualWidth - 50),
                    random.NextDouble() * (canvas.ActualHeight - 50));
            }

            // клонирование дерева
            Shape tree = new ShapeBuilder(ShapeBuilder.Type.TREE).Build();
            tree.Stroke = Brushes.Blue;
            double left = random.NextDouble() * (canvas.ActualWidth - 50);
            double top = random.NextDouble() * (canvas.ActualHeight - 50);
            AddToCanvas(tree, left, top);

            for (int j = 0; j < 2; j++) {
                left += tree.Width * 0.5;
                top += tree.Height * 0;

                Tree copy = (tree as Tree).Clone() as Tree; // копия
                copy.Height = copy.Height * 1;
                copy.Width = copy.Width * 0.8;
                copy.Stroke = Brushes.Green;
                AddToCanvas(copy, left, top);
            }
        }

        private void AddToCanvas(Shape shape, double left, double top) {
            shape.SetValue(Canvas.LeftProperty, left);
            shape.SetValue(Canvas.TopProperty, top);
            shape.Cursor = Cursors.Hand;
            shape.MouseLeftButtonUp += Shape_MouseLeftButtonUp;
            canvas.Children.Add(shape);
        }

        private void Shape_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Logger.Log($"Элемент: {Util.GetDescription(sender)}");
        }

        // Создать и нарисовать композит
        private void BtnComposite_Click(object sender, RoutedEventArgs e) {
            Composite picture = new Composite("picture", 0, 0);

            Composite background = new Composite("background", 0, 0);
            background.AddComponent(new ShapeComponent("border1", 0, 0, new Rectangle() { Fill = Brushes.Black, Width = 206, Height = 206 }));
            background.AddComponent(new ShapeComponent("border2", 1, 1, new Rectangle() { Fill = Brushes.LightCoral, Width = 204, Height = 204 }));
            background.AddComponent(new ShapeComponent("page", 3, 3, new Rectangle() { Fill = Brushes.White, Width = 200, Height = 200 }));
            picture.AddComponent(background);

            Composite bushes = new Composite("bushes", 0, 150);
            bushes.AddComponent(new ShapeComponent("bush_1", 0, 0, new Tree() { Stroke = Brushes.LightGreen, Width = 100, Height = 50, Levels = 8, StrokeThickness = 2, TrunkFactor = 0.2, BranchFactor = 0.9 }));
            bushes.AddComponent(new ShapeComponent("bush_2", 40, 0, new Tree() { Stroke = Brushes.Green, Width = 100, Height = 50, Levels = 8, StrokeThickness = 2, TrunkFactor = 0.2, BranchFactor = 0.9 }));
            bushes.AddComponent(new ShapeComponent("bush_3", 80, 0, new Tree() { Stroke = Brushes.LightGreen, Width = 100, Height = 50, Levels = 8, StrokeThickness = 2, TrunkFactor = 0.2, BranchFactor = 0.9 }));
            bushes.AddComponent(new ShapeComponent("bush_4", 120, 0, new Tree() { Stroke = Brushes.Green, Width = 100, Height = 50, Levels = 8, StrokeThickness = 2, TrunkFactor = 0.2, BranchFactor = 0.9 }));
            picture.AddComponent(bushes);

            picture.AddComponent(new ShapeComponent("main_tree", 0, 0, new Tree() { Stroke = Brushes.Blue, Width = 200, Height = 200, Levels = 10, StrokeThickness = 2, TrunkFactor = 0.34, BranchFactor = 0.66, DeltaAngle = 25 }));

            picture.Draw(canvas, canvas.ActualWidth * 0.5 - 100, 50);

            IComponent component = picture.Find("main_tree");
            Logger.Log($"По имени 'main_tree' был найден компонент {component.Title}");
        }
    }
}
