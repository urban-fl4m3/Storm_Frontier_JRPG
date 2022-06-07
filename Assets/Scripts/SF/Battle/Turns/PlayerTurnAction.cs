using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Game;
using SF.UI.Controller;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private readonly BattleHUDController _battleHUDController;

        private Actor _actingActor;
        
        public PlayerTurnAction(IServiceLocator services, BattleWorld world, BattleHUDController battleHUDController) 
            : base(services, world)
        {
            _battleHUDController = battleHUDController;
        }
        
        public override void MakeTurn(BattleActor actor)
        {
            _actingActor = actor;
            
            _battleHUDController.ShowHUD();
            
            _battleHUDController.AttackSelected += HandleAttackSelected;
            _battleHUDController.SkillSelected += HandleSkillSelected;
            _battleHUDController.ItemSelected += HandleItemSelected;
            _battleHUDController.GuardSelected += HandleGuardSelected;
        }

        private void HandleAttackSelected()
        {
            Debug.Log("Attack!");
            
            //Add actor enemy selector
            //Show attack animation
            // _actingActor.
            CompleteTurn();
        }
        
        private void HandleSkillSelected(int skillIndex)
        {
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
            Debug.Log("Guard!");
            CompleteTurn();
        }

        protected override void Dispose()
        {
            _battleHUDController.AttackSelected -= HandleAttackSelected;
            _battleHUDController.SkillSelected -= HandleSkillSelected;
            _battleHUDController.ItemSelected -= HandleItemSelected;
            _battleHUDController.GuardSelected -= HandleGuardSelected;
            _battleHUDController.HideHUD();
        }
    }
}