using Lab15.Model.Factory;
using Lab15.Utils;

namespace Lab15.Commands {

    class YoungerCommand : ICommand {

        public void Execute() {

            Logger.Log("Команда: YoungerCommand");

            StarSingletonFactory.GetStar().Younger();
        }
    }
}
