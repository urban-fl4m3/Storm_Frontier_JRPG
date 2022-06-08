using System;
using System.Collections.Generic;
using SF.Battle.Actors;

namespace SF.Battle.TargetSelection
{
    public class NoTargetSelectionRule : ITargetSelectionRule
    {
        public event Action<BattleActor> TargetSelected;
        
        public void TrackSelection(IEnumerable<BattleActor> actors)
        {
            TargetSelected?.Invoke(null);    
        }
    }
}