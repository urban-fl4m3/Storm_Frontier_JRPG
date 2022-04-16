using SF.Common.Logger;
using SF.Common.States;

namespace SF.Game.States
{
    public abstract class GameState : IState
    {
        protected IWorld World { get; }
        protected IDebugLogger Logger { get; }

        protected GameState(IWorld world, IDebugLogger logger)
        {
            World = world;
            Logger = logger;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        protected abstract void OnEnter();
        protected abstract void OnExit();
    }
}