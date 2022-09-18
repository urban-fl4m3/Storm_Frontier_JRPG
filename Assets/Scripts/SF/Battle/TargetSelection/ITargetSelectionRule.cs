using System.Collections.Generic;
using SF.Battle.Actors;

namespace SF.Battle.TargetSelection
{
    public interface ITargetSelectionRule
    {
        BattleActor[] GetPossibleTargets(IEnumerable<BattleActor> actors);
    }
}