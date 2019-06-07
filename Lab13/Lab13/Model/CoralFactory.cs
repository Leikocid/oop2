using Lab13.Utils;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab13.Model {
    public class CoralFactory : AbstractShapeFactory {
        public override Shape CreateEllipse() {
            Shape result = new Ellipse() {
                Width = 30,
                Height = 30,
                Fill = Brushes.Coral
            };

            Logger.Log("Кораловая фабрика: Создается элипс");

            return result;
        }

        public override Shape CreateRectangle() {
            Shape result = new Rectangle() {
                Width = 30,
                Height = 30,
                Fill = Brushes.Coral
            };

            Logger.Log("Кораловая фабрика: Создается прямоугольник");

            return result;
        }
    }
}
