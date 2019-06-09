using Lab15.Model.Factory;
using Lab15.Model.Memento;
using Lab15.Model.States;
using Lab15.Utils;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace Lab15.Model {

    public class GoldStar : Shape, IHasDescriprion {

        private Shape star;

        public GoldStar() {
            Width = 50;
            Height = 50;
            Stroke = Brushes.Black;
            star = new ShapeBuilder(ShapeBuilder.Type.STAR).Build();

            RenderTransform = new ScaleTransform(1, 1, star.Width / 2, star.Height / 2);
            Effect = new BlurEffect() { Radius = 0 };
            Fill = new SolidColorBrush(Colors.LightYellow);

            State = new Golden(this);
        }

        public AbstractStarState State { set; get; }

        public void Younger() {
            State.Younger(this);
        }

        public void Older() {
            State.Older(this);
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
            return $"Единственная в своем роде золотая звезда высотой {Height}. Состояние - {State.Info}";
        }

        // сохранение и восстановление состояния (Memento)
        public StarMemento SaveState() {

            Logger.Log("Хранитель: Создается копия состояния звезды");

            return new StarMemento(State, (double)GetValue(Canvas.LeftProperty), (double)GetValue(Canvas.TopProperty));
        }

        public void RestoreState(StarMemento memento) {

            Logger.Log("Хранитель: Состояние звезды восстановлено");

            State = memento.State;
            State.AnimateNewState(this);

            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5)); 
            animation.Completed += (sender, e) => {
                Canvas.SetLeft(this, memento.Left);
                Canvas.SetTop(this, memento.Top);
                BeginAnimation(OpacityProperty, new DoubleAnimation(1, TimeSpan.FromSeconds(0.5)));
            };
            BeginAnimation(OpacityProperty, animation);
       }
    }
}
