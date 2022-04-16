using SF.Game.Player;

namespace SF.Game.Initializers
{
    public interface IWorldInitializer
    {
        IWorld GetWorld(IServiceLocator serviceLocator, IPlayerState playerState);
    }
}