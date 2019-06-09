using Lab15.Utils;
using System.Windows.Media;

namespace Lab15.Model.States {

    class WhiteDwarf : AbstractStarState {

        public override string Info => "Белый карлик. Предыдущее состояние - Красный гигант. При старени становится черным карликом.";

        public WhiteDwarf(GoldStar star) : base(star) { }

        public override void Older(GoldStar star) {

            Logger.Log("Состояние: переход в BlackDwarf.");

            star.State = new BlackDwarf(star);
        }

        public override void Younger(GoldStar star) {

            Logger.Log("Состояние: переход в RedGiant.");

            star.State = new RedGiant(star);
        }

        public override void AnimateNewState(GoldStar star) {
            AnimateBlur(star, 0);
            AnimateScale(star, 0.5);
            AnimateColor(star, Colors.White);
        }
    }
}
