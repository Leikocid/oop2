using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab15.Model {
    class UnderlinedShape : Shape, IHasDescriprion {

        // underline shape decorator
        private  Shape shape;
        public UnderlinedShape(Shape shape) {
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

                // рисуем подчеркивание
                result.Children.Add(new LineGeometry(new Point(0, Height + 3), new Point(Width, Height + 3)));
                return result;
            }
        }

        public string GetDescription() {
            return $"Декоратор подчеркивания для элемента: {new ShapeToIDescriprionAdapter(shape).GetDescription()}.";
        }
    }
}
