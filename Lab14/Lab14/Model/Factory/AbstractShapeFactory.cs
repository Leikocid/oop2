using System.Windows.Shapes;

namespace Lab14.Model.Factory {
    public abstract class AbstractShapeFactory {
        public abstract Shape CreateRectangle();
        public abstract Shape CreateEllipse();
    }
}
