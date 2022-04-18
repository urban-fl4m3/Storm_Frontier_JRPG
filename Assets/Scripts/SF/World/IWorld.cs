using SF.Game.Player;

namespace SF.Game
{
    public interface IWorld
    {
        IServiceLocator ServiceLocator { get; }
        IPlayerState PlayerState { get; }

        void Run();
    }
}