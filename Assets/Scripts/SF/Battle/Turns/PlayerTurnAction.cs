using System;
using Cysharp.Threading.Tasks;
using SF.Battle.Abilities;
using SF.Battle.Common;
using SF.Battle.Field;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Camera;
using SF.Game;
using SF.UI.Controller;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private readonly BattleField _field;
        private readonly PlayerTurnModel _model;
        private readonly ISmartCameraRegistrar _cameraHolder;
        private readonly BattleActorRegistrar _actorsRegistrar;
        private readonly PlayerActionsViewController _playerActionsViewController;

        public PlayerTurnAction(
            BattleField field,
            ISmartCameraRegistrar cameraHolder,
            BattleActorRegistrar actorsRegistrar,
            PlayerActionsViewController playerActionsViewController)
        {
            _field = field;
            _cameraHolder = cameraHolder;
            _actorsRegistrar = actorsRegistrar;
            _playerActionsViewController = playerActionsViewController;
            
            _model = new PlayerTurnModel(actorsRegistrar);
        }

        protected override void OnStartTurn()
        {
            RenderActiveActor();
            SetupCamera();
            
            _field.GetActiveActorPlaceholder().PlaceActor(ActingActor);

            _playerActionsViewController.ShowView();
            _playerActionsViewController.SetCurrentActor(ActingActor);

            SubscribeOnPlayerInput();
        }

        protected override void Dispose()
        {
            _model.Cancel();
            
            _field.GetActiveActorPlaceholder().Release();
            _field.ResetTeamPlaceholders(ActingActor.Team);
            _playerActionsViewController.HideView();

            UnsubscribeFromPlayerInput();
            
            var camera = _cameraHolder.GetMainCamera();
            camera.Clear();
        }

        private void RenderActiveActor()
        {
            foreach (var actor in _actorsRegistrar.GetTeamActors(Team.Player))
            {
                var isActingActor = actor == ActingActor;
                actor.SetVisibility(isActingActor);
            }
        }

        private void SetupCamera()
        {
            var cinemachineComponent = ActingActor.Components.Get<CinemachineTargetComponent>();

            var camera = _cameraHolder.GetMainCamera();
            camera.SetPosition(cinemachineComponent.CameraPosition);
            camera.SetTarget(cinemachineComponent.LookAtPosition, 0);
            camera.SetTarget(_field.Center, 1);
        }

        private void SubscribeOnPlayerInput()
        {
            _playerActionsViewController.AttackSelected += HandleAttackSelected;
            _playerActionsViewController.SkillSelected += HandleSkillSelected;
            _playerActionsViewController.ItemSelected += HandleItemSelected;
            _playerActionsViewController.GuardSelected += HandleGuardSelected;
        }

        private void UnsubscribeFromPlayerInput()
        {
            _playerActionsViewController.AttackSelected -= HandleAttackSelected;
            _playerActionsViewController.SkillSelected -= HandleSkillSelected;
            _playerActionsViewController.ItemSelected -= HandleItemSelected;
            _playerActionsViewController.GuardSelected -= HandleGuardSelected;
        }

        private void HandleAttackSelected()
        {

            var attackSelectionData = new TargetSelectionData(TargetPick.OppositeTeam);
            var attackSelectionRule = new TargetSelectionRule(ActingActor, attackSelectionData);
            
            MakeAsyncAction(attackSelectionRule,  
                () => ActingActor.PerformAttack(_model.SelectedActor, CompleteTurn))
                .Forget();
        }

        private void HandleSkillSelected(ActiveBattleAbilityData abilityData)
        {
            var abilityComponent = ActingActor.Components.Get<AbilityComponent>();

            if (!abilityComponent.CanInvoke(abilityData))
            {
                return;
            }

            var skillSelectionData = new TargetSelectionData(abilityData.Pick);
            var skillSelectionRule = new TargetSelectionRule(ActingActor, skillSelectionData);
            
            MakeAsyncAction(skillSelectionRule,
                () =>  ActingActor.PerformSkill(abilityData, _model.SelectedActor, CompleteTurn))
                .Forget();
        }

        private void HandleItemSelected(int itemIndex)
        {
            Debug.Log($"Item {itemIndex}!");
            CompleteTurn();
        }
        
        private void HandleGuardSelected()
        {
            var guardSelectionData = new TargetSelectionData(TargetPick.Instant);
            var guardSelectionRule = new TargetSelectionRule(ActingActor, guardSelectionData);

            MakeAsyncAction(guardSelectionRule, 
                () => ActingActor.PerformGuard(CompleteTurn))
                .Forget();
        }

        private async UniTaskVoid MakeAsyncAction(ITargetSelectionRule selectionRule, Action action)
        {
            _model.Cancel();
            _model.SetSelectionRules(selectionRule);
            
            await _model.TargetSelectedCompletionSource.Task;

            if (_model.SelectedActor != null)
            {
                var camera =  _cameraHolder.GetMainCamera();
                camera.SetTarget(_model.SelectedActor.Components.Get<CinemachineTargetComponent>().LookAtPosition, 1);
            }

            action?.Invoke();
            Dispose();
        }
    }
}