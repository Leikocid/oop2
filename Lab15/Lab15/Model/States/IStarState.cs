namespace Lab15.Model.States {

    public interface IStarState {

        string Info { get; }

        void Younger(GoldStar star);
        void Older(GoldStar star);
    }
}
