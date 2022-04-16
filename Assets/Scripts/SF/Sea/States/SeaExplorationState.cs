using SF.Common.Data;
using SF.Game;
using SF.Game.States;

namespace SF.Sea.States
{
    public class SeaExplorationState : GameState
    {
        public SeaExplorationState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public override void Enter(IDataProvider data)
        {
            ServiceLocator.Logger.Log("Entered sea exploration state");
        }

        public override void Exit()
        {
            ServiceLocator.Logger.Log("Exited sea exploration state");
        }
    }
}