using Lab13.Utils;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab13.Model {
    
    class TreeShape : Shape, ICloneable {

        public TreeShape() {
            Width = 50;
            Height = 50;
            StrokeThickness = 1;
        }

        private PathGeometry tree = null;
        protected override Geometry DefiningGeometry {
            get {
                if (tree == null) {
                    PathGeometry result = new PathGeometry();
                    MakeTreeGeometry(result, Width /2, Height, Height / 3, 90, 6);
                    tree = result;
                }
                return tree;
            }
        }

        public new double Height {
            get {
                return base.Height;
            }
            set {
                base.Height = value;
                tree = null;
            }
        }

        public new double Width {
            get {
                return base.Width;
            }
            set {
                base.Width = value;
                tree = null;
            }
        }

        private readonly double deltaAngle = 20; // последующие ветки ротклоняются на 15 градусов
        private readonly double koeff = 0.65; // как быстро укорачиваются ветки

        private void MakeTreeGeometry(PathGeometry tree, double x1, double y1, double L,  double angle, int level) {
            double x2 = x1 + L * Math.Cos(angle * Math.PI / 180);
            double y2 = y1 - L * Math.Sin(angle * Math.PI / 180);
            tree.AddGeometry(new LineGeometry(new Point(x1, y1), new Point(x2, y2)));

            if (level > 1) {
                MakeTreeGeometry(tree, x2, y2, L * koeff, angle + deltaAngle, level - 1);
                MakeTreeGeometry(tree, x2, y2, L * koeff, angle - deltaAngle, level - 1);
            }
        }

        public object Clone() {

            Logger.Log("Прототип: Создание копии дерева");

            return new TreeShape() {
                Height = this.Height,
                Width = this.Width,
                Fill = this.Fill,
                Stroke = this.Stroke
            };
        }
    }
}
