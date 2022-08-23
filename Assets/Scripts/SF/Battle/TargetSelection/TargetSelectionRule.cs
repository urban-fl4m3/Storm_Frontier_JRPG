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
        public event Action<BattleActor> TargetSelected;

        private readonly BattleActor _actingActor;
        private readonly TargetSelectionData _data;
        
        public TargetSelectionRule(BattleActor actingActor, TargetSelectionData data)
        {
            _data = data;
            _actingActor = actingActor;
        }

        public BattleActor[] GetPossibleTargets(IEnumerable<BattleActor> actors)
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

            return expectedActors;
        }

        public void TrackSelection(IEnumerable<BattleActor> actors)
        {
            
            // foreach (var enemy in expectedActors)
            // {
                // enemy.Components.Get<ActorSelectComponent>().ActorSelected += OnActorSelected;
            // }

            // void OnActorSelected(IActor actor)
            // {
            //     if (actor.Components.Get<ActorStateComponent>().State.Value == ActorState.Dead) return;
            //     if (!(actor is BattleActor battleActor)) return;
            //
            //     foreach (var expected in expectedActors)
            //     {
            //         expected.Components.Get<ActorSelectComponent>().ActorSelected -= OnActorSelected;
            //     }
            //     
            //     TargetSelected?.Invoke(battleActor);
            // }
        }
    }
}