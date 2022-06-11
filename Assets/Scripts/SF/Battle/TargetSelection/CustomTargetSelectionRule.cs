using System;
using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Common.Actors;

namespace SF.Battle.TargetSelection
{
    public class CustomTargetSelectionRule : ITargetSelectionRule
    {
        public event Action<IActor> TargetSelected;

        private readonly CustomTargetSelectionData _data;
        
        public CustomTargetSelectionRule(CustomTargetSelectionData data)
        {
            _data = data;
        }
        
        public void TrackSelection(IEnumerable<BattleActor> actors)
        {
            
        }
    }
}