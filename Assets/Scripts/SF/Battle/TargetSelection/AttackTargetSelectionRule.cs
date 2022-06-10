using System;
using System.Collections.Generic;
using System.Linq;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Actors.Components.Status;

namespace SF.Battle.TargetSelection
{
    public class AttackTargetSelectionRule : ITargetSelectionRule
    {
        public event Action<IActor> TargetSelected;

        private readonly BattleActor _actingActor;

        public AttackTargetSelectionRule(BattleActor actingActor)
        {
            _actingActor = actingActor;
        }

        public void TrackSelection(IEnumerable<BattleActor> actors)
        {
            var enemies = actors.Where(x => x.Team != _actingActor.Team);
            var battleActors = enemies as BattleActor[] ?? enemies.ToArray();

            foreach (var enemy in battleActors)
            {
                enemy.Components.Get<ActorSelectComponent>().ActorSelected += OnActorSelected;
            }

            void OnActorSelected(IActor actor)
            {
                if (actor.Components.Get<ActorStateComponent>().State.Value == ActorState.Dead) return;

                foreach (var battleActor in battleActors)
                {
                    battleActor.Components.Get<ActorSelectComponent>().ActorSelected -= OnActorSelected;
                }
                
                TargetSelected?.Invoke(actor);
            }
        }
    }
}