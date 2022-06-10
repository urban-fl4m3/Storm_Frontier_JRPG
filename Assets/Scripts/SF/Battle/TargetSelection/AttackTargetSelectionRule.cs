using System;
using System.Collections.Generic;
using System.Linq;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Actors.Components.Stats;
using Sirenix.Utilities;
using UnityEngine;

namespace SF.Battle.TargetSelection
{
    public class AttackTargetSelectionRule : ITargetSelectionRule
    {
        public event Action<BattleActor> TargetSelected;

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
                enemy.Components.Get<ActorSelectComponent>().ActorSelected += (x) =>
                {
                    if (!(x is BattleActor battleActor)) return;
                    if (x.Components.Get<HealthComponent>().CurrentHealth.Value == 0) return; 
                    
                    battleActors.ForEach(y => y.Components.Get<ActorSelectComponent>().Clear());
                    TargetSelected?.Invoke(battleActor);
                };
            }
        }
    }
}