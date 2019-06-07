using Lab13.Utils;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab13.Model {
    public class ShapeBuilder {

        public enum Type { RECRANGLE = 1, ELLIPSE, STAR, DIAMOND, TREE }

        private readonly Type type = Type.RECRANGLE;
        private Brush color = Brushes.Black;
        private double height = 30;
        private double width = 30;

        public ShapeBuilder() {
        }

        public ShapeBuilder(Type type) {
            this.type = type;
        }

        public ShapeBuilder SetWidth(double width) {
            this.width = width;
            return this;
        }

        public ShapeBuilder SetHeight(double height) {
            this.height = height;
            return this;
        }

        public ShapeBuilder SetColor(Brush color) {
            this.color = color;
            return this;
        }

        public Shape Build() {
            Shape result;
            switch (type) {
                case Type.RECRANGLE: {
                        result = new Rectangle();

                        Logger.Log("Строитель: Создается прямоугольник");

                        break;
                    }
                case Type.ELLIPSE: {
                        result = new Ellipse();

                        Logger.Log("Строитель: Создается элипс");

                        break;
                    }
                case Type.STAR: {
                        result = new Polygon {
                            Points = new PointCollection(new[] { new Point(10, 2), new Point(11.5, 7), new Point(17, 7), new Point(12, 10), new Point(14, 15), new Point(9, 12), new Point(4, 15), new Point(6, 10), new Point(1, 7), new Point(7, 7) })
                        };

                        Logger.Log("Строитель: Создается звезда");

                        break;
                    }
                case Type.TREE: {
                        result = new TreeShape();

                        Logger.Log("Строитель: Создается дерево");

                        break;
                    }
                default: {
                        result = new Polygon {
                            Points = new PointCollection(new[] { new Point(1, 7), new Point(9, 13), new Point(17, 7), new Point(9, 1) })
                        };

                        Logger.Log("Строитель: Создается ромб");

                        break;
                    }
            }
            result.Stroke = Brushes.Black;
            result.Stretch = Stretch.Fill;
            result.Width = width;
            result.Height = height;
            result.Fill = color;
            return result;
        }
    }
}

