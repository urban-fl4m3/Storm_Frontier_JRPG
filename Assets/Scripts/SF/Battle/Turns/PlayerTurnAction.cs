using Cysharp.Threading.Tasks;
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
        private readonly PlayerActionsViewController _playerActionsViewController;
        private readonly PlayerTurnModel _model;
        
        public PlayerTurnAction(IServiceLocator services, BattleWorld world, PlayerActionsViewController playerActionsViewController) 
            : base(services, world)
        {
            _playerActionsViewController = playerActionsViewController;
            _model = new PlayerTurnModel(world);
        }

        protected override void OnStartTurn(BattleActor actor)
        {
            _model.CurrentActor = actor;

            _playerActionsViewController.ShowView();
            _playerActionsViewController.SetCurrentActor(actor);
            
            _playerActionsViewController.AttackSelected += HandleAttackSelected;
            _playerActionsViewController.SkillSelected += HandleSkillSelected;
            _playerActionsViewController.ItemSelected += HandleItemSelected;
            _playerActionsViewController.GuardSelected += HandleGuardSelected;
        }
        
        protected override void Dispose()
        {
            _playerActionsViewController.HideView();
            
            _playerActionsViewController.AttackSelected -= HandleAttackSelected;
            _playerActionsViewController.SkillSelected -= HandleSkillSelected;
            _playerActionsViewController.ItemSelected -= HandleItemSelected;
            _playerActionsViewController.GuardSelected -= HandleGuardSelected;
        }

        private void HandleAttackSelected()
        {
            _model.Cancel();

            var attackSelectionData = new TargetSelectionData(TargetPick.OppositeTeam);
            var attackSelectionRule = new TargetSelectionRule(_model.CurrentActor, attackSelectionData);
            _model.SetSelectionRules(attackSelectionRule);
            
            AttackAsync().Forget();
        }

        private void HandleSkillSelected(BattleAbilityData abilityData)
        {
            _model.Cancel();

            var abilityComponent = _model.CurrentActor.Components.Get<AbilityComponent>();
            var ability = abilityComponent.GetAbilityData(abilityData);
            var skillSelectionData = new TargetSelectionData(ability.Pick);
            var skillSelectionRule = new TargetSelectionRule(_model.CurrentActor, skillSelectionData);
            _model.SetSelectionRules(skillSelectionRule);

            UseAbilityAsync(abilityData).Forget();
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
            await _model.TargetSelectedCompletionSource.Task;

            Dispose();

            _model.CurrentActor.PerformAttack(_model.SelectedActor, CompleteTurn);
        }

        private async UniTaskVoid UseAbilityAsync(BattleAbilityData abilityData)
        {
            await _model.TargetSelectedCompletionSource.Task;
            
            Dispose();

            _model.CurrentActor.PerformSkill(abilityData, _model.SelectedActor, CompleteTurn);
        }

        private async UniTask GuardAsync()
        {
            await _model.TargetSelectedCompletionSource.Task;
            
            Dispose();

            _model.CurrentActor.PerformGuard(CompleteTurn);
        }
    }
}