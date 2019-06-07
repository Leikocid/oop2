using System.Windows.Shapes;

namespace Lab13.Model {
    public abstract class AbstractShapeFactory {
        public abstract Shape CreateRectangle();
        public abstract Shape CreateEllipse();
    }
}
