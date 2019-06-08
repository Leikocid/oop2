using Lab14.Utils;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab14.Model.Factory {

    public class WhiteFactory : AbstractShapeFactory {
        public override Shape CreateEllipse() {
            Shape result = new Ellipse() {
                Width = 50,
                Height = 50,
                Fill = Brushes.White
            };

            Logger.Log("Бклая фабрика: Создается элипс");

            return result;
        }

        public override Shape CreateRectangle() {
            Shape result = new Rectangle() {
                Width = 50,
                Height = 50,
                Fill = Brushes.White
            };

            Logger.Log("Белая фабрика: Создается прямоугльник");

            return result;
        }
    }
}
