using Lab15.Utils;
using System.Windows.Media;

namespace Lab15.Model.States {
    class ProtoStar : AbstractStarState {

        public override string Info => "Протозвезда. При взрослении становится обычным золотой звездой.";

        public ProtoStar(GoldStar star) : base(star) { }

        public override void Older(GoldStar star) {

            Logger.Log("Состояние: переход в Golden.");

            star.State = new Golden(star);
        }

        public override void Younger(GoldStar star) { }

        public override void AnimateNewState(GoldStar star) {
            AnimateBlur(star, star.Height * 0.2);
            AnimateScale(star, 1.2);
            AnimateColor(star, Colors.Orange);
        }
    }
}
