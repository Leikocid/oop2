using Lab15.Utils;
using System.Windows.Media;

namespace Lab15.Model.States {
    class RedGiant : AbstractStarState {

        public override string Info => "Красный гигант. Предыдущее состояние - залотая звезда. При старении становится белым карликом.";

        public RedGiant(GoldStar star) : base(star) { }

        public override void Older(GoldStar star) {

            Logger.Log("Состояние: переход в WhiteDwarf.");

            star.State = new WhiteDwarf(star);
        }

        public override void Younger(GoldStar star) {

            Logger.Log("Состояние: переход в Golden.");

            star.State = new Golden(star);
        }

        public override void AnimateNewState(GoldStar star) {
            AnimateBlur(star, 0);
            AnimateScale(star, 2);
            AnimateColor(star, Colors.Red);
        }
    }
}
