using System;
using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Common.Actors;

namespace SF.Battle.TargetSelection
{
    public class NoTargetSelectionRule : ITargetSelectionRule
    {
        public event Action<IActor> TargetSelected;
        
        public void TrackSelection(IEnumerable<BattleActor> actors)
        {
            TargetSelected?.Invoke(null);    
        }
    }
}