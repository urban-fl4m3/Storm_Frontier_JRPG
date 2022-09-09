using SF.Common.Data;
using SF.Common.States;
using SF.Game.Player;

namespace SF.Game.States
{
    public abstract class GameState : IState
    {
        protected IServiceLocator Services { get; }
        protected IPlayerState PlayerState { get; }

        protected GameState(IServiceLocator services, IPlayerState playerState)
        {
            Services = services;
            PlayerState = playerState;
        }

        public abstract void Enter(IDataProvider data);

        public abstract void Exit();
    }
}