using Lab15.Model.Factory;
using Lab15.Utils;

namespace Lab15.Commands {

    public class OlderCommand: ICommand {

        public void Execute() {

            Logger.Log("Команда: OlderCommand");

            StarSingletonFactory.GetStar().Older();
        }
    }
}
