using System;
using System.Collections;
using System.Linq;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Logger;
using SF.Game.Common;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SF.Battle.Turns
{
    public class AiTurnAction : BaseTurnAction
    {
        private readonly IDebugLogger _logger;
        
        private IDisposable _temporaryDelaySub;

        public AiTurnAction(BattleActor actor, IDebugLogger logger, IBattleActorsHolder actorsHolder) 
            : base(actor, actorsHolder)
        {
            _logger = logger;
        }

        protected override void OnSelectionStepStart()
        {
            _temporaryDelaySub = Observable.FromCoroutine(CalculatePoints).Subscribe();
        }

        protected override void OnSelectionStepFinish()
        { 
            if (ActingActor != null)
            {
                ActingActor.Components.Get<PlaceholderComponent>().SetSelected(false);
            }

            _temporaryDelaySub?.Dispose();
        }

        protected override void OnActionStepStart()
        {
            
        }

        protected override void OnActionStepFinish()
        {
            
        }
        
        private IEnumerator CalculatePoints()
        {
            yield return new WaitForSeconds(1);
            _temporaryDelaySub.Dispose();

            var chanceToUseSkill = Random.Range(0, 100);
            BattleActor target;
            Action<BattleActor> selectedAction;
            float actionTime = Constants.Battle.MinCastTime;

            if (chanceToUseSkill >= 30)
            {
                var abilityComponent = ActingActor.Components.Get<AbilityComponent>();
                var abilities = abilityComponent.AbilitiesData.ToArray();

                var randomAbilityIndex = Random.Range(0, abilities.Length);
                var randomAbility = abilities[randomAbilityIndex];

                target = SelectRandomTarget(randomAbility.Pick);
                selectedAction = a => ActingActor.PerformSkill(randomAbility, a);
                actionTime = randomAbility.CastTime;
            }
            else
            {
                target = SelectRandomTarget(TargetPick.OppositeTeam);
                selectedAction = ActingActor.PerformAttack;
            }
            
            PickTarget(target);
            SetActionTime(actionTime);
            SelectActionToPerform(selectedAction);
            RaiseActionSelected(true);
        }

        private BattleActor SelectRandomTarget(TargetPick pick)
        {
            if (pick == TargetPick.Instant)
            {
                return ActingActor;
            }

            var actors = ActorsHolder.GetAllActors().Where(x =>
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