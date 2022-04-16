using SF.Game.Player;

namespace SF.Game
{
    public abstract class BaseWorld : IWorld
    {
        public IServiceLocator ServiceLocator { get; }
        public IPlayerState PlayerState { get; }

        protected BaseWorld(IServiceLocator serviceLocator, IPlayerState playerState)
        {
            ServiceLocator = serviceLocator;
            PlayerState = playerState;
        }
    }
}