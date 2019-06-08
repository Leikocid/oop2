using Lab14.Utils;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab14.Model.Factory {

    public sealed class StarSingletonFactory {

        /// <summary>
        /// Thread-safe singleton
        /// </summary>
        private static Shape instance = null;
        private static readonly object padlock = new object();

        public static Shape GetShape {
            get {

                Logger.Log("Одиночка: Запрошен синглтон (простая реализация)");

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
        private static readonly Lazy<Shape> lazy = new Lazy<Shape>(() => CreateStar());

        public static Shape GetStar() {

            Logger.Log("Одиночка: Запрошен синглтон (реализация с помощью lazy)");

            return lazy.Value;
        }

        private static Shape CreateStar() {
            Shape result = new GoldStar();

            Logger.Log("Одинчка: Создан новый синглтон");

            return result;
        }
    }
}
