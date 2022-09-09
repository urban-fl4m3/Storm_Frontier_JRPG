using System;
using System.Collections.Generic;
using SF.Battle.Actors;

namespace SF.Battle.TargetSelection
{
    public interface ITargetSelectionRule
    {
        event Action<BattleActor> TargetSelected;

        void TrackSelection(IEnumerable<BattleActor> actors);
        BattleActor[] GetPossibleTargets(IEnumerable<BattleActor> actors);
    }
}