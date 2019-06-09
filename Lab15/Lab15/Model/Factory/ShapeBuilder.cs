using Lab15.Utils;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab15.Model.Factory {
    public class ShapeBuilder {

        public enum Type { RECRANGLE = 1, ELLIPSE, STAR, DIAMOND, TREE }

        private readonly Type type = Type.RECRANGLE;
        private Brush color = Brushes.Black;
        private double height = 50;
        private double width = 50;
        private bool underline = false;
        private bool cross = false;
        private bool border = false;

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

        public ShapeBuilder Underline(bool underline) {
            this.underline = underline;
            return this;
        }

        public ShapeBuilder Cross(bool cross) {
            this.cross = cross;
            return this;
        }

        public ShapeBuilder Border(bool border) {
            this.border = border;
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
                        double w = width / 17;
                        double h = height / 15;
                        result = new Polygon {
                            Points = new PointCollection(new[] { new Point(9 * w, 0* h), new Point(11.4 * w, 6 * h), new Point(17 * w, 6 * h), new Point(12 * w, 10* h),
                                new Point(14* w, 15* h), new Point(9* w, 12* h), new Point(4* w, 15* h), new Point(6* w, 10* h), new Point(1* w, 6 * h), new Point(7* w, 6 * h) })
                        };

                        Logger.Log("Строитель: Создается звезда");

                        break;
                    }
                case Type.TREE: {
                        result = new Tree();

                        Logger.Log("Строитель: Создается дерево");

                        break;
                    }
                default: {
                        double w = width / 50;
                        double h = height / 50;
                        result = new Polygon {
                            Points = new PointCollection(new[] { new Point(12 * w, 25 * h), new Point(25 * w, 50 * h), new Point(38 * w, 25 * h), new Point(25 * w, 0 * h) })
                        };

                        Logger.Log("Строитель: Создается ромб");

                        break;
                    }
            }
            if (underline) {
                result = new UnderlinedShape(result);
            }
            if (cross) {
                result = new CrossedShape(result);
            }
            if (border) {
                result = new BorderedShape(result);
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

