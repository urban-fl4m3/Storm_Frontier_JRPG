using SF.Game.Player;

namespace SF.Game
{
    public abstract class BaseWorld : IWorld
    {
        public IPlayerState PlayerState { get; }
        
        protected IServiceLocator ServiceLocator { get; }

        protected BaseWorld(IServiceLocator serviceLocator, IPlayerState playerState)
        {
            ServiceLocator = serviceLocator;
            PlayerState = playerState;
        }
        
        public abstract void Run();
    }
}