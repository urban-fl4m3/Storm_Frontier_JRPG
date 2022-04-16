using SF.Common.Logger;
using SF.Game;
using SF.Game.States;

namespace SF.Sea.States
{
    public class SeaExplorationState : GameState
    {
        public SeaExplorationState(IWorld world, IDebugLogger logger) : base(world, logger)
        {
        }

        protected override void OnEnter()
        {
            Logger.Log("Entered sea exploration state");
        }

        protected override void OnExit()
        {
            Logger.Log("Exited sea exploration state");
        }
    }
}