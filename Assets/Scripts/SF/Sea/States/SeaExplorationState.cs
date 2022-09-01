using SF.Common.Data;
using SF.Game;
using SF.Game.Player;
using SF.Game.States;

namespace SF.Sea.States
{
    public class SeaExplorationState : GameState
    {
        public SeaExplorationState(IServiceLocator services, IPlayerState playerState) : base(services, playerState)
        {
        }

        public override void Enter(IDataProvider data)
        {
            Services.Logger.Log("Entered sea exploration state");
        }

        public override void Exit()
        {
            Services.Logger.Log("Exited sea exploration state");
        }
    }
}