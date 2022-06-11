using System;
using System.Collections.Generic;
using System.Linq;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Actors.Components.Status;

namespace SF.Battle.TargetSelection
{
    public class TargetSelectionRule : ITargetSelectionRule
    {
        public event Action<IActor> TargetSelected;

        private readonly BattleActor _actingActor;
        private readonly TargetSelectionData _data;
        
        public TargetSelectionRule(BattleActor actingActor, TargetSelectionData data)
        {
            _data = data;
            _actingActor = actingActor;
        }

        public void TrackSelection(IEnumerable<BattleActor> actors)
        {
            var expectedPick = _data.Pick;

            if (expectedPick == TargetPick.Instant)
            {
                TargetSelected?.Invoke(null);
            }

            var expectedActors = actors as BattleActor[] ?? actors.ToArray();

            if (expectedPick == TargetPick.AllyTeam)
            {
                expectedActors = expectedActors
                    .Where(x => x.Team == _actingActor.Team)
                    .ToArray();
            }
            else if (expectedPick == TargetPick.OppositeTeam)
            {
                expectedActors = expectedActors
                    .Where(x => x.Team != _actingActor.Team)
                    .ToArray();
            }
            
            foreach (var enemy in expectedActors)
            {
                enemy.Components.Get<ActorSelectComponent>().ActorSelected += OnActorSelected;
            }

            void OnActorSelected(IActor actor)
            {
                if (actor.Components.Get<ActorStateComponent>().State.Value == ActorState.Dead) return;

                foreach (var battleActor in expectedActors)
                {
                    battleActor.Components.Get<ActorSelectComponent>().ActorSelected -= OnActorSelected;
                }
                
                TargetSelected?.Invoke(actor);
            }
        }
    }
}