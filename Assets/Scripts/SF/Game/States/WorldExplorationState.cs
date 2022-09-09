using SF.Common.Data;
using SF.Game.Player;

namespace SF.Game.States
{
    public class WorldExplorationState : GameState
    {
        public WorldExplorationState(IServiceLocator services, IPlayerState playerState) : base(services, playerState)
        {
        }

        public override void Enter(IDataProvider data)
        {
            Services.Logger.Log("Entered world exploration state");
        }

        public override void Exit()
        {
            Services.Logger.Log("Exited world exploration state");
        }
    }
}