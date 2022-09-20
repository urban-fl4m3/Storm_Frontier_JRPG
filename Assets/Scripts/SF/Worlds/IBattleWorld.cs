using SF.Battle.Common;
using SF.Battle.Field;
using SF.Battle.Turns;

namespace SF.Game.Worlds
{
    public interface IBattleWorld : IWorld
    {
        IBattleActorsHolder ActorsHolder { get; }
        TurnManager Turns { get; }
        BattleField Field { get; }
    }
}