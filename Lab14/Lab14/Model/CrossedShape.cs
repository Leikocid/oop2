using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab14.Model {
    class CrossedShape : Shape, IHasDescriprion {

        // underline shape decorator
        private Shape shape;
        public CrossedShape(Shape shape) {
            this.shape = shape;
        }

        protected override Geometry DefiningGeometry {
            get {
                GeometryGroup result = new GeometryGroup();
                // рисуем встроенный элемент
                shape.Height = Height;
                shape.Width = Width;
                shape.Measure(new Size(Width, Height));
                shape.Arrange(new Rect(new Size(Width, Height)));
                result.Children.Add(shape.RenderedGeometry);

                // рисуем перечеркивание
                result.Children.Add(new LineGeometry(new Point(0, 0), new Point(Width, Height)));
                result.Children.Add(new LineGeometry(new Point(0, Height), new Point(Width, 0)));
                return result;
            }
        }

        public string GetDescription() {
            return $"Декоратор перечеркивания для элемента: {new ShapeToIDescriprionAdapter(shape).GetDescription()}.";
        }
    }
}