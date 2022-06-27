﻿using Cysharp.Threading.Tasks;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Battle.TargetSelection;
using SF.Common.Actors.Abilities;
using SF.Game;
using SF.UI.Controller;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private readonly PlayerActionsController _playerActionsController;
        private readonly PlayerTurnModel _model;
        
        public PlayerTurnAction(IServiceLocator services, BattleWorld world, PlayerActionsController playerActionsController) 
            : base(services, world)
        {
            _playerActionsController = playerActionsController;
            _model = new PlayerTurnModel(world);
        }

        protected override void OnStartTurn(BattleActor actor)
        {
            _model.CurrentActor = actor;

            _playerActionsController.ShowView();
            _playerActionsController.SetCurrentActor(actor);
            
            _playerActionsController.AttackSelected += HandleAttackSelected;
            _playerActionsController.SkillSelected += HandleSkillSelected;
            _playerActionsController.ItemSelected += HandleItemSelected;
            _playerActionsController.GuardSelected += HandleGuardSelected;
        }
        
        protected override void Dispose()
        {
            _playerActionsController.AttackSelected -= HandleAttackSelected;
            _playerActionsController.SkillSelected -= HandleSkillSelected;
            _playerActionsController.ItemSelected -= HandleItemSelected;
            _playerActionsController.GuardSelected -= HandleGuardSelected;
            _playerActionsController.HideView();
        }

        private void HandleAttackSelected()
        {
            _model.Cancel();

            var attackSelectionData = new TargetSelectionData(TargetPick.OppositeTeam);
            var attackSelectionRule = new TargetSelectionRule(_model.CurrentActor, attackSelectionData);
            _model.SetSelectionRules(attackSelectionRule);
            
            AttackAsync().Forget();
        }

        private void HandleSkillSelected(string abilityName)
        {
            _model.Cancel();

            var ability = _model.CurrentActor.Components.Get<AbilityComponent>().GetBattleAbility(abilityName);
            var skillSelectionData = new TargetSelectionData(ability.Pick);
            var skillSelectionRule = new TargetSelectionRule(_model.CurrentActor, skillSelectionData);
            _model.SetSelectionRules(skillSelectionRule);

            UseAbilityAsync(abilityName).Forget();
        }

        private void HandleItemSelected(int itemIndex)
        {
            Debug.Log($"Item {itemIndex}!");
            CompleteTurn();
        }
        
        private void HandleGuardSelected()
        {
            _model.Cancel();
            
            var guardSelectionData = new TargetSelectionData(TargetPick.Instant);
            var guardSelectionRule = new TargetSelectionRule(_model.CurrentActor, guardSelectionData);
            _model.SetSelectionRules(guardSelectionRule);
            
            GuardAsync().Forget();
        }

        private async UniTaskVoid AttackAsync()
        {
            await _model.taskCompletionSource.Task;

            Dispose();
            
            var activeActor = _model.CurrentActor;
            activeActor.PerformAttack(_model.SelectedActor, CompleteTurn);
        }

        private async UniTaskVoid UseAbilityAsync(string abilityName)
        {
            await _model.taskCompletionSource.Task;
            
            Dispose();

            var activeActor = _model.CurrentActor;
            activeActor.PerformSkill(abilityName, _model.SelectedActor, CompleteTurn);
        }

        private async UniTask GuardAsync()
        {
            await _model.taskCompletionSource.Task;
            
            var activeActor = _model.CurrentActor;
            activeActor.PerformGuard(CompleteTurn);
        }
    }
}