using System.Windows.Controls;

namespace Lab14.Model.Composite {

    public abstract class AbstractComponent : IComponent {

        public AbstractComponent(string title, double left, double top) {
            Title = title;
            Left = left;
            Top = top;
        }

        public string Title { get; set; }
        public double Left { get; set; }
        public double Top { get; set; }

        public abstract void Draw(Canvas canvas, double left, double top);
        public abstract IComponent Find(string title);
    }
}
