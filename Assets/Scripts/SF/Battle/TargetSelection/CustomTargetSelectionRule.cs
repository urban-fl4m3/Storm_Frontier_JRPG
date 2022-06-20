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
        private readonly CustomTargetSelectionData _data;
        
        public CustomTargetSelectionRule(BattleActor actingeActor, CustomTargetSelectionData data)
        {
            _actingeActor = actingeActor;
            _data = data;
        }
        
        public void TrackSelection(IEnumerable<BattleActor> actors)
        {
            if (_data.IsInstant)
            {
                if (_data.SelfSelect)
                {
                    TargetSelected?.Invoke(_actingeActor);
                    return;
                }

                if (_data.SelectAll)
                {
                    
                }
            }
        }
    }
}