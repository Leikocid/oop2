using Lab15.Utils;
using System.Windows.Media;

namespace Lab15.Model.States {
    class Golden : AbstractStarState {

        public override string Info => "Золотая звезда. Предыдущее состояние - протозвезда. При старении становится красным гигантом.";

        public Golden(GoldStar star) : base(star) { }

        public override void Older(GoldStar star) {

            Logger.Log("Состояние: переход в RedGiant.");

            star.State = new RedGiant(star);
        }

        public override void Younger(GoldStar star) {

            Logger.Log("Состояние: переход в ProtoStar.");

            star.State = new ProtoStar(star);
        }

        public override void AnimateNewState(GoldStar star) {
            AnimateBlur(star, 0);
            AnimateScale(star, 1);
            AnimateColor(star, Colors.LightYellow);
        }
    }
}
