using SF.Game.Player;

namespace SF.Game
{
    public class DefaultWorld : BaseWorld
    {
        public DefaultWorld(IServiceLocator serviceLocator, IPlayerState playerState) : base(serviceLocator, playerState)
        {
            
        }
    }
}