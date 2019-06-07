using Lab13.Utils;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab13.Model {

    public sealed class StarSingletonFactory {

        /// <summary>
        /// Thread-safe singleton
        /// </summary>
        private static Polygon instance = null;
        private static readonly object padlock = new object();

        public static Shape GetShape {
            get {
                if (instance == null) {
                    lock (padlock) {
                        if (instance == null) {
                            instance = CreateStar();
                        }
                    }
                }
                return instance;
            }
        }


        /// <summary>
        /// Lazy
        /// </summary>
        private static readonly Lazy<Polygon> lazy = new Lazy<Polygon>(() => CreateStar());

        public static Shape GetStar() {

            Logger.Log("Одинчка: Запрошен синглтон");

            return lazy.Value;
        }

        private static Polygon CreateStar() {
            Polygon result = new Polygon {
                Fill = Brushes.LightYellow,
                Points = new PointCollection(new[] { new Point(10, 2), new Point(11.5, 7), new Point(17, 7), new Point(12, 10), new Point(14, 15), new Point(9, 12), new Point(4, 15), new Point(6, 10), new Point(1, 7), new Point(7, 7) }),
                Stroke = Brushes.Black,
                Width = 30,
                Height = 30,
                Stretch = Stretch.Fill
            };

            Logger.Log("Одинчка: Создан новый синглтон");

            return result;
        }
    }
}
