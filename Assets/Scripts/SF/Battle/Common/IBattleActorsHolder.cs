using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Game;

namespace SF.Battle.Common
{
    public interface IBattleActorsHolder
    {
        BattleActor ActingActor { get; }
        IEnumerable<BattleActor> Actors { get; }

        IEnumerable<BattleActor> GetTeamActors(Team team);
        IEnumerable<BattleActor> GetOppositeTeamActors(Team team);
    }
}