using SF.Common.Logger;

namespace SF.Game.States
{
    public class WorldExplorationState : GameState
    {
        public WorldExplorationState(IWorld world, IDebugLogger logger) : base(world, logger)
        {
        }

        protected override void OnEnter()
        {
            Logger.Log("Entered world exploration state");
        }

        protected override void OnExit()
        {
            Logger.Log("Exited world exploration state");
        }
    }
}