using Lab14.Utils;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab14.Model {

    class Tree : Shape, ICloneable, IHasDescriprion {

        public Tree() {
            Stretch = Stretch.Fill;
        }

        public int Levels { get; set; } = 6;               // количество уровней ветвистости
        public double TrunkFactor { get; set; } = 0.3;     // относительная длина ствола
        public double BranchFactor { get; set; } = 0.65;   // как быстро укорачиваются ветки
        public int DeltaAngle { get; set; } = 15;          // отклонение в градусах для последующих веток

        protected override Geometry DefiningGeometry {
            get {
                PathGeometry result = new PathGeometry();
                MakeTreeGeometry(result, Width * 0.5, Height, Height * TrunkFactor, 90, Levels);
                return result;
            }
        }

        private void MakeTreeGeometry(PathGeometry tree, double x1, double y1, double length, double angle, int level) {
            double x2 = x1 + length * Math.Cos(angle * Math.PI / 180);
            double y2 = y1 - length * Math.Sin(angle * Math.PI / 180);
            tree.AddGeometry(new LineGeometry(new Point(x1, y1), new Point(x2, y2)));

            if (level > 1) {
                MakeTreeGeometry(tree, x2, y2, length * BranchFactor, angle + DeltaAngle, level - 1);
                MakeTreeGeometry(tree, x2, y2, length * BranchFactor, angle - DeltaAngle, level - 1);
            }
        }

        // клонирование
        public object Clone() {

            Logger.Log("Прототип: Создание копии дерева");

            Tree copy = new Tree() { };
            // копируем параметры
            copy.Height = this.Height;
            copy.Width = this.Width;
            copy.Fill = this.Fill;
            copy.Stroke = this.Stroke;
            copy.Levels = this.Levels;
            copy.Stretch = this.Stretch;
            copy.TrunkFactor = this.TrunkFactor;
            copy.BranchFactor = this.BranchFactor;
            copy.DeltaAngle = this.DeltaAngle;

            return copy;
        }

        public string GetDescription() {
            return $"Дерево высотой {Height}.";
        }
    }
}
