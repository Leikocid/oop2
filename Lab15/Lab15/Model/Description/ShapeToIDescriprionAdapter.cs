using System.Windows.Shapes;

namespace Lab15.Model {
    public class ShapeToIDescriprionAdapter : IHasDescriprion {

        private Shape shape;
        public ShapeToIDescriprionAdapter(Shape shape) {
            this.shape = shape;
        }

        public string GetDescription() {
            if (shape is IHasDescriprion) {
                return (shape as IHasDescriprion).GetDescription();
            } else {
                return $"Фигура высотой {shape.Height}";
            }
        }
    }
}
