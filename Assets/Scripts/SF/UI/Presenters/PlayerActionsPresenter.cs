using System;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Common.Data;
using SF.Game;
using SF.UI.Models.Actions;
using SF.UI.View;

namespace SF.UI.Presenters
{
    public class PlayerActionsPresenter : BaseBattlePresenter<PlayerActionButtonsView>
    {
        private BattleActor _currentActor;
        private IDisposable _activeActorObserver;

        public PlayerActionsPresenter(
            PlayerActionButtonsView view,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder)
            : base(view, world, serviceLocator, actionBinder)
        {
            
        }

        public override void Enable()
        {
            View.AttackButton.onClick.AddListener(OnAttackClick);
            View.SkillButton.onClick.AddListener(OnSkillClick);
            View.UseItemButton.onClick.AddListener(OnItemClick);
            View.GuardButton.onClick.AddListener(OnGuardClick);
            
            View.Show();
        }

        public override void Disable()
        {
            View.AttackButton.onClick.RemoveListener(OnAttackClick);
            View.SkillButton.onClick.RemoveListener(OnSkillClick);
            View.UseItemButton.onClick.RemoveListener(OnItemClick);
            View.GuardButton.onClick.RemoveListener(OnGuardClick);
            
            View.Hide();
        }

        private void OnAttackClick()
        {
            View.HideAbility();
            RaiseAction(ActionName.Attack);
        }

        private void OnSkillClick()
        {
            View.ShowAbility();
            View.SubscribeOnAbilities(World.ActingActor, OnSkillSelected);
        }

        private void OnSkillSelected(ActiveBattleAbilityData data)
        {
            RaiseAction(ActionName.Skills, new DataProvider(data));
        }

        private void OnItemClick()
        {
            View.HideAbility();
            RaiseAction(ActionName.Item, new DataProvider(0));
        }

        private void OnGuardClick()
        {
            View.HideAbility();
            RaiseAction(ActionName.Guard);
        }
    }
}