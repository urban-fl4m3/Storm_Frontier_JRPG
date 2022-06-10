using System;
using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Common.Actors;

namespace SF.Battle.TargetSelection
{
    public interface ITargetSelectionRule
    {
        event Action<IActor> TargetSelected;

        void TrackSelection(IEnumerable<BattleActor> actors);
    }
}