using System;
using System.Collections;
using System.Linq;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Game;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SF.Battle.Turns
{
    public class AiTurnAction : BaseTurnAction
    {
        private IDisposable _temporaryDelaySub;

        public AiTurnAction(IServiceLocator services, BattleWorld world) : base(services, world)
        {
        }

        protected override void OnStartTurn()
        {
            Services.Logger.Log($"Actor {ActingActor} turn completed");

            ActingActor.Components.Get<PlaceholderComponent>().SetSelected(true);

            _temporaryDelaySub = Observable.FromCoroutine(CalculatePoints).Subscribe();
        }

        protected override void Dispose()
        {
            if (ActingActor != null)
            {
                ActingActor.Components.Get<PlaceholderComponent>().SetSelected(false);
            }

            _temporaryDelaySub?.Dispose();
        }

        private IEnumerator CalculatePoints()
        {
            yield return new WaitForSeconds(1);
            _temporaryDelaySub.Dispose();

            var chanceToUseSkill = Random.Range(0, 100);

            if (chanceToUseSkill >= 30)
            {
                var abilityComponent = ActingActor.Components.Get<AbilityComponent>();
                var abilities = abilityComponent.AbilitiesData.ToArray();

                var randomAbilityIndex = Random.Range(0, abilities.Length);
                var randomAbility = abilities[randomAbilityIndex];

                var target = SelectRandomTarget(randomAbility.Pick);
                
                ActingActor.PerformSkill(randomAbility, target, CompleteTurn);
            }
            else
            {
                var target = SelectRandomTarget(TargetPick.OppositeTeam);
                ActingActor.PerformAttack(target, CompleteTurn);
            }
        }

        private IActor SelectRandomTarget(TargetPick pick)
        {
            if (pick == TargetPick.Instant)
            {
                return null;
            }

            var actors = World.ActingActors.Where(x =>
            {
                if (pick == TargetPick.AllyTeam)
                {
                    return x.Team == ActingActor.Team;
                }

                if (pick == TargetPick.OppositeTeam)
                {
                    return x.Team != ActingActor.Team;
                }
                
                return true;
            }).ToList();

            var randomActorIndex = Random.Range(0, actors.Count);
            var randomActor = actors[randomActorIndex];

            return randomActor;
        }
    }
}