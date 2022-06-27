using SF.Game.Player;

namespace SF.Game
{
    public interface IWorld
    {
        IPlayerState PlayerState { get; }
    }
}