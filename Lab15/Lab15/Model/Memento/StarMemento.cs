using Lab15.Model.States;

namespace Lab15.Model.Memento {
    public class StarMemento {

        public StarMemento(AbstractStarState state, double left, double top) {
            State = state;
            Left = left;
            Top = top;
        }

        public AbstractStarState State { get; private set; }
        public double Left { get; private set; }
        public double Top { get; private set; }
    }
}
