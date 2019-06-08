using System.Windows.Controls;

namespace Lab14.Model.Composite {

    public interface IComponent {

        string Title { get; set; }
        void Draw(Canvas canvas, double left, double top);
        IComponent Find(string title);

    }
}
