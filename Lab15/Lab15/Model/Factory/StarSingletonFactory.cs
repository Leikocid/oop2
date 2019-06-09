using Lab15.Utils;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab15.Model.Factory {

    public sealed class StarSingletonFactory {

        /// <summary>
        /// Thread-safe singleton
        /// </summary>
        private static GoldStar instance = null;
        private static readonly object padlock = new object();

        public static GoldStar GetShape {
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
        private static readonly Lazy<GoldStar> lazy = new Lazy<GoldStar>(() => CreateStar());

        public static GoldStar GetStar() {

            Logger.Log("Одиночка: Запрошен синглтон (реализация с помощью lazy)");

            return lazy.Value;
        }

        private static GoldStar CreateStar() {
            GoldStar result = new GoldStar();

            Logger.Log("Одиночка: Создан новый синглтон");

            return result;
        }
    }
}
