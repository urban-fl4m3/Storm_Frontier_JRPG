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
        
        public PlayerTurnAction(IServiceLocator services, BattleWorld world, PlayerActionsViewController playerActionsViewController) 
            : base(services, world)
        {
            _playerActionsViewController = playerActionsViewController;
            _model = new PlayerTurnModel(world);
        }

        protected override void OnStartTurn()
        {
            ActingActor.Components.Get<PlaceholderComponent>().SetSelected(true);
            

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
        }

        private void HandleAttackSelected()
        {
            _model.Cancel();

            var attackSelectionData = new TargetSelectionData(TargetPick.OppositeTeam);
            var attackSelectionRule = new TargetSelectionRule(ActingActor, attackSelectionData);
            _model.SetSelectionRules(attackSelectionRule);
            
            AttackAsync().Forget();
        }

        private void HandleSkillSelected(ActiveBattleAbilityData abilityData)
        {
            var abilityComponent = ActingActor.Components.Get<AbilityComponent>();

            if (!abilityComponent.CanInvoke(abilityData))
            {
                return;
            }
            
            _model.Cancel();

            var skillSelectionData = new TargetSelectionData(abilityData.Pick);
            var skillSelectionRule = new TargetSelectionRule(ActingActor, skillSelectionData);
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
            var guardSelectionRule = new TargetSelectionRule(ActingActor, guardSelectionData);
            _model.SetSelectionRules(guardSelectionRule);
            
            GuardAsync().Forget();
        }

        private async UniTaskVoid AttackAsync()
        {
            await _model.TargetSelectedCompletionSource.Task;

            Dispose();

            ActingActor.PerformAttack(_model.SelectedActor, CompleteTurn);
        }

        private async UniTaskVoid UseAbilityAsync(ActiveBattleAbilityData abilityData)
        {
            await _model.TargetSelectedCompletionSource.Task;
            
            Dispose();

            ActingActor.PerformSkill(abilityData, _model.SelectedActor, CompleteTurn);
        }

        private async UniTask GuardAsync()
        {
            await _model.TargetSelectedCompletionSource.Task;
            
            Dispose();

            ActingActor.PerformGuard(CompleteTurn);
        }
    }
}