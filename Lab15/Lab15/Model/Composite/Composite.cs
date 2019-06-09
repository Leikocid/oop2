using System.Collections.Generic;
using System.Windows.Controls;

namespace Lab15.Model.Composite {

    public class Composite : AbstractComponent {

        List<AbstractComponent> children = new List<AbstractComponent>();

        public Composite(string title, double left, double top) : base(title, left, top) { }


        public void AddComponent(AbstractComponent component) {
            children.Add(component);
        }

        public void RemoveComponent(AbstractComponent component) {
            children.Remove(component);
        }

        public override void Draw(Canvas canvas, double left, double top) {
            children.ForEach(c => c.Draw(canvas, left + c.Left, top + c.Top));
        }

        public override IComponent Find(string title) {
            if (title.Equals(Title)) {
                return this;
            }

            return children.Find(c => c.Find(title) != null);
        }
    }
}
