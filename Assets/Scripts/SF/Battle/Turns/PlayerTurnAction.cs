using System;
using Cysharp.Threading.Tasks;
using SF.Battle.Abilities;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Game;
using SF.UI.Controller;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private readonly PlayerActionsViewController _playerActionsViewController;
        private readonly PlayerTurnModel _model;

        public PlayerTurnAction(IServiceLocator services, BattleWorld world,
            PlayerActionsViewController playerActionsViewController)
            : base(services, world)
        {
            _playerActionsViewController = playerActionsViewController;
            _model = new PlayerTurnModel(world);
        }

        protected override void OnStartTurn()
        {
            ActingActor.Components.Get<PlaceholderComponent>().SetSelected(true);
            var cinemachineComponent = ActingActor.Components.Get<CinemachineTargetComponent>();

            var camera = Services.CameraHolder.GetMainCamera();
            camera.SetPosition(cinemachineComponent.CameraPosition);
            camera.SetTarget(cinemachineComponent.LookAtPosition, 0);

            _playerActionsViewController.ShowView();
            _playerActionsViewController.SetCurrentActor(ActingActor);

            _playerActionsViewController.AttackSelected += HandleAttackSelected;
            _playerActionsViewController.SkillSelected += HandleSkillSelected;
            _playerActionsViewController.ItemSelected += HandleItemSelected;
            _playerActionsViewController.GuardSelected += HandleGuardSelected;
        }

        protected override void Dispose()
        {
            _model.Cancel();

            ActingActor.Components.Get<PlaceholderComponent>().SetSelected(false);

            _playerActionsViewController.HideView();

            _playerActionsViewController.AttackSelected -= HandleAttackSelected;
            _playerActionsViewController.SkillSelected -= HandleSkillSelected;
            _playerActionsViewController.ItemSelected -= HandleItemSelected;
            _playerActionsViewController.GuardSelected -= HandleGuardSelected;
            
            var camera = Services.CameraHolder.GetMainCamera();
            camera.Clear();
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

        private async UniTaskVoid MakeAsyncAction(TargetSelectionRule selectionRule, Action action)
        {
            _model.Cancel();
            _model.SetSelectionRules(selectionRule);
            
            await _model.TargetSelectedCompletionSource.Task;

            if (_model.SelectedActor != null)
            {
                var camera =  Services.CameraHolder.GetMainCamera();
                camera.SetTarget(_model.SelectedActor.Components.Get<CinemachineTargetComponent>().LookAtPosition, 1);
            }

            action?.Invoke();
            Dispose();
        }
    }
}