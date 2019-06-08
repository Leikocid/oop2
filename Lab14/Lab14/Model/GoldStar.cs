using Lab14.Model.Factory;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Lab14.Model {

    public class GoldStar : Shape, IHasDescriprion {

        private Shape star;
        public GoldStar() {
            Width = 50;
            Height = 50;
            Stroke = Brushes.Black;
            Fill = Brushes.LightYellow;
            star = new ShapeBuilder(ShapeBuilder.Type.STAR).Build();
        }

        protected override Geometry DefiningGeometry {
            get {
                GeometryGroup result = new GeometryGroup();
                star.Height = Height;
                star.Width = Width;
                star.Measure(new Size(Width, Height));
                star.Arrange(new Rect(new Size(Width, Height)));
                result.Children.Add(star.RenderedGeometry);
                return result;
            }
        }

        public string GetDescription() {
            return $"Единственная в своем роде золотая звезда высотой {Height}.";
        }
    }
}
