using SF.Common.Data;
using SF.Common.States;

namespace SF.Game.States
{
    public abstract class GameState : IState
    {
        protected IServiceLocator ServiceLocator { get; }

        protected GameState(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }

        public abstract void Enter(IDataProvider data);

        public abstract void Exit();
    }
}