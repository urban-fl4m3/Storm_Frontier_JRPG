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
        private readonly BattleHUDController _battleHUDController;
        private readonly PlayerTurnModel _model;
        
        public PlayerTurnAction(IServiceLocator services, BattleWorld world, BattleHUDController battleHUDController) 
            : base(services, world)
        {
            _battleHUDController = battleHUDController;
            _model = new PlayerTurnModel(world);
        }
        
        public override void MakeTurn(BattleActor actor)
        {
            _model.CurrentActor = actor;
            
            _battleHUDController.ShowHUD();
            
            _battleHUDController.AttackSelected += HandleAttackSelected;
            _battleHUDController.SkillSelected += HandleSkillSelected;
            _battleHUDController.ItemSelected += HandleItemSelected;
            _battleHUDController.GuardSelected += HandleGuardSelected;
        }
        
        protected override void Dispose()
        {
            _battleHUDController.AttackSelected -= HandleAttackSelected;
            _battleHUDController.SkillSelected -= HandleSkillSelected;
            _battleHUDController.ItemSelected -= HandleItemSelected;
            _battleHUDController.GuardSelected -= HandleGuardSelected;
            _battleHUDController.HideHUD();
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
            await UniTask.WaitWhile(() => _model.SelectedActor is null, cancellationToken: _model.CancelationToken.Token);

            Dispose();
            
            var activeActor = _model.CurrentActor;
            activeActor.PerformAttack(_model.SelectedActor, CompleteTurn);
        }

        private async UniTask GuardAsync()
        {
            await UniTask.WaitWhile(() => _model.SelectedActor is null, cancellationToken: _model.CancelationToken.Token);
            
            var activeActor = _model.CurrentActor;
            activeActor.PerformGuard(CompleteTurn);
        }
    }
}