using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Game;

namespace SF.Battle.Common
{
    public interface IRegisteredActorsHolder
    {
        IEnumerable<BattleActor> ActingActors { get; }

        IEnumerable<BattleActor> GetTeamActors(Team team);
        IEnumerable<BattleActor> GetOppositeTeamActors(Team team);
    }
}