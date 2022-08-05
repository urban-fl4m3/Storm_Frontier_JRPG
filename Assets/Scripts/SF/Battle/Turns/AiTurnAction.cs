using System;
using System.Collections;
using System.Linq;
using SF.Battle.Common;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Camera;
using SF.Common.Logger;
using SF.Game;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SF.Battle.Turns
{
    public class AiTurnAction : BaseTurnAction
    {
        private readonly IDebugLogger _logger;
        private readonly ISmartCameraRegistrar _cameraHolder;
        
        private IDisposable _temporaryDelaySub;

        public AiTurnAction(IDebugLogger logger, IBattleActorsHolder actorsHolder, ISmartCameraRegistrar cameraHolder) 
            : base(actorsHolder)
        {
            _logger = logger;
            _cameraHolder = cameraHolder;
        }

        protected override void OnStartTurn()
        {
            _logger.Log($"Actor {ActingActor} turn completed");

            RenderAllActors();
            
            var cinemachineComponent = ActingActor.Components.Get<CinemachineTargetComponent>();
            var playerLookAtPosition = ActorsHolder.GetTeamActors(Team.Player).FirstOrDefault()
                ?.Components
                .Get<CinemachineTargetComponent>().LookAtPosition;

            ActingActor.Components.Get<PlaceholderComponent>().SetSelected(true);

            var camera = _cameraHolder.GetMainCamera();
            
            camera.SetPosition(cinemachineComponent.CameraPosition);
            camera.SetTarget(cinemachineComponent.LookAtPosition, 0);
            camera.SetTarget(playerLookAtPosition, 1);

            _temporaryDelaySub = Observable.FromCoroutine(CalculatePoints).Subscribe();
        }

        protected override void OnTurnComplete()
        {
            if (ActingActor != null)
            {
                ActingActor.Components.Get<PlaceholderComponent>().SetSelected(false);
            }

            _temporaryDelaySub?.Dispose();
        }

        private void RenderAllActors()
        {
            foreach (var actor in ActorsHolder.ActingActors)
            {
                actor.SetVisibility(true);
            }
        }
        
        private IEnumerator CalculatePoints()
        {
            yield return new WaitForSeconds(1);
            _temporaryDelaySub.Dispose();

            var chanceToUseSkill = Random.Range(0, 100);
            SceneActor target;

            if (chanceToUseSkill >= 30)
            {
                var abilityComponent = ActingActor.Components.Get<AbilityComponent>();
                var abilities = abilityComponent.AbilitiesData.ToArray();

                var randomAbilityIndex = Random.Range(0, abilities.Length);
                var randomAbility = abilities[randomAbilityIndex];

                target = SelectRandomTarget(randomAbility.Pick);
                ActingActor.PerformSkill(randomAbility, target, CompleteTurn);
            }
            else
            {
                target = SelectRandomTarget(TargetPick.OppositeTeam);
                ActingActor.PerformAttack(target, CompleteTurn);
            }
            
            SetTarget(target);
        }

        private SceneActor SelectRandomTarget(TargetPick pick)
        {
            if (pick == TargetPick.Instant)
            {
                return ActingActor;
            }

            var actors = ActorsHolder.ActingActors.Where(x =>
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

        private void SetTarget(IActor actor)
        {
            var lookAtPosition = actor.Components.Get<CinemachineTargetComponent>().LookAtPosition;

            var camera = _cameraHolder.GetMainCamera();
            camera.SetTarget(lookAtPosition, 1);
        }
    }
}