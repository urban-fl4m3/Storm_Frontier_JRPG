using SF.Common.Data;

namespace SF.Game.States
{
    public class WorldExplorationState : GameState
    {
        public WorldExplorationState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public override void Enter(IDataProvider data)
        {
            ServiceLocator.Logger.Log("Entered world exploration state");
        }

        public override void Exit()
        {
            ServiceLocator.Logger.Log("Exited world exploration state");
        }
    }
}