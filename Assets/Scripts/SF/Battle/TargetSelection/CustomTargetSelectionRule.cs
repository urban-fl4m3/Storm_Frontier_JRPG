using System;
using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Common.Actors;

namespace SF.Battle.TargetSelection
{
    public class CustomTargetSelectionRule : ITargetSelectionRule
    {
        public event Action<IActor> TargetSelected;

        private readonly BattleActor _actingeActor;
        private readonly TargetSelectionData _data;
        
        public CustomTargetSelectionRule(BattleActor actingeActor, TargetSelectionData data)
        {
            _actingeActor = actingeActor;
            _data = data;
        }
        
        public void TrackSelection(IEnumerable<BattleActor> actors)
        {
            
        }
    }
}