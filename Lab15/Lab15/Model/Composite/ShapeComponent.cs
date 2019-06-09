using Lab15.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab15.Model.Composite {

    class ShapeComponent : AbstractComponent {

        public ShapeComponent(string title, double left, double top, Shape shape): base(title, left, top) {
            Shape = shape;
        }

        public Shape Shape { get; set; }

        public override void Draw(Canvas canvas, double left, double top) {
            Shape.SetValue(Canvas.LeftProperty, left);
            Shape.SetValue(Canvas.TopProperty, top);
            Shape.Cursor = Cursors.Hand;
            Shape.MouseLeftButtonDown += Shape_MouseLeftButtonDown;
            canvas.Children.Add(Shape);
        }

        private void Shape_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            Logger.Log($"ShapeComponent {Title}", Brushes.Brown);
        }

        public override IComponent Find(string title) {
            if (title != null && title.Equals(Title)) {
                return this;
            }

            return null;
        }
    }
}
