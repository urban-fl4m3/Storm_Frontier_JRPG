using Cysharp.Threading.Tasks;
using SF.Battle.Actors;
using SF.Battle.TargetSelection;
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
            _model.SetSelectionRules(new AttackTargetSelectionRule(_model.CurrentActor));
            
            AttackAsync().Forget();
        }

        private void HandleSkillSelected(int skillIndex)
        {
            _model.Cancel();
            
            Debug.Log($"Skill {skillIndex}!");
            CompleteTurn();
        }

        private void HandleItemSelected(int itemIndex)
        {
            Debug.Log($"Item {itemIndex}!");
            CompleteTurn();
        }
        
        private void HandleGuardSelected()
        {
            _model.Cancel();
            _model.SetSelectionRules(new NoTargetSelectionRule());
            
            GuardAsync().Forget();
        }

        private async UniTask AttackAsync()
        {
            await _model.taskCompletionSource.Task;

            Dispose();
            
            var activeActor = _model.CurrentActor;
            activeActor.PerformAttack(_model.SelectedActor, CompleteTurn);
        }

        private async UniTask GuardAsync()
        {
            await _model.taskCompletionSource.Task;
            
            var activeActor = _model.CurrentActor;
            activeActor.PerformGuard(CompleteTurn);
        }
    }
}