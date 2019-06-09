using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab15.Model {
    class BorderedShape : Shape, IHasDescriprion {

        // underline shape decorator
        private Shape shape;
        public BorderedShape(Shape shape) {
            this.shape = shape;
        }

        protected override Geometry DefiningGeometry {
            get {
                GeometryGroup result = new GeometryGroup();
                shape.Height = Height;
                shape.Width = Width;
                shape.Measure(new Size(Width, Height));
                shape.Arrange(new Rect(new Size(Width, Height)));
                result.Children.Add(shape.RenderedGeometry);

                // рисуем рамку
                result.Children.Add(new LineGeometry(new Point(0, 0), new Point(0, Height)));
                result.Children.Add(new LineGeometry(new Point(0, 0), new Point(Width, 0)));
                result.Children.Add(new LineGeometry(new Point(0, Height), new Point(Width, Height)));
                result.Children.Add(new LineGeometry(new Point(Width, 0), new Point(Width, Height)));
                return result;
            }
        }

        public string GetDescription() {
            return $"Декоратор рамки для элемента: {new ShapeToIDescriprionAdapter(shape).GetDescription()}.";
        }
    }
}
