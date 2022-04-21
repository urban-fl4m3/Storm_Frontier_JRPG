using SF.Game.Player;

namespace SF.Game
{
    public abstract class BaseWorld : IWorld
    {
        protected IServiceLocator ServiceLocator { get; }
        protected IPlayerState PlayerState { get; }

        protected BaseWorld(IServiceLocator serviceLocator, IPlayerState playerState)
        {
            ServiceLocator = serviceLocator;
            PlayerState = playerState;
        }
        
        public abstract void Run();
    }
}