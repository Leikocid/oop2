using Lab13.Utils;
using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab13.Model {
    // Drunk DIRECTOR
    public class RandomFactory {

        private Random random = new Random();

        public RandomFactory() {
        }

        public Shape BuildRandomShape() {

            Logger.Log("Директор: Создается случайная фигура при помощи Строителя фигур");

            return new ShapeBuilder((ShapeBuilder.Type)random.Next(1, 6)).SetColor(Brushes.LightBlue).SetWidth(random.Next(15, 40)).SetHeight(random.Next(15, 40)).Build();
        }
    }
}
