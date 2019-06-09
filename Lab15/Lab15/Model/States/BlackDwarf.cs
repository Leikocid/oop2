using Lab15.Utils;
using System.Windows.Media;

namespace Lab15.Model.States {
    class BlackDwarf : AbstractStarState {

        public override string Info => "Черный карлик. Предыдущее состояние - белый карлик. В нашей программе стареть больше не может";

        public BlackDwarf(GoldStar star) : base(star) { }

        public override void Older(GoldStar star) {

        }

        public override void Younger(GoldStar star) {

            Logger.Log("Состояние: переход в WhiteDwarf.");

            star.State = new WhiteDwarf(star);
        }

        public override void AnimateNewState(GoldStar star) {
            AnimateBlur(star, 0);
            AnimateScale(star, 0.2);
            AnimateColor(star, Colors.Black);
        }
    }
}
