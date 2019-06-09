using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace Lab15.Model.States {
    public abstract class AbstractStarState : IStarState {

        public abstract string Info { get; }

        public abstract void Older(GoldStar star);
        public abstract void Younger(GoldStar star);

        public AbstractStarState(GoldStar star) {
            AnimateNewState(star);
        }

        public abstract void AnimateNewState(GoldStar star);

        public void AnimateScale(GoldStar star, double toScale) {
            DoubleAnimation animation = new DoubleAnimation (toScale, TimeSpan.FromSeconds(1)) {
                EasingFunction = new CubicEase() {
                    EasingMode = EasingMode.EaseInOut
                }
            };
            ScaleTransform transform = star.RenderTransform as ScaleTransform;
            transform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            transform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        public void AnimateBlur(GoldStar star, double toRadius) {
            (star.Effect as BlurEffect).BeginAnimation(BlurEffect.RadiusProperty, new DoubleAnimation(toRadius, TimeSpan.FromSeconds(1)));
        }

        public void AnimateColor(GoldStar star, Color toColor) {
            star.Fill.BeginAnimation(SolidColorBrush.ColorProperty, new ColorAnimation (toColor, TimeSpan.FromSeconds(1)));
        }
    }
}
