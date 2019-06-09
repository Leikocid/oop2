using System.Windows.Shapes;

namespace Lab15.Model.Factory {
    public abstract class AbstractShapeFactory {
        public abstract Shape CreateRectangle();
        public abstract Shape CreateEllipse();
    }
}
