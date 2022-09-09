using SF.Battle.Common;
using SF.Battle.Field;

namespace SF.Game.Worlds
{
    public interface IBattleWorld : IWorld
    {
        IBattleActorsHolder ActorsHolder { get; }
        BattleField Field { get; }
    }
}