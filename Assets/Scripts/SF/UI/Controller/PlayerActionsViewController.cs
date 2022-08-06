using System;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Common.Data;
using SF.Game;
using SF.UI.View;

namespace SF.UI.Controller
{
    public class PlayerActionsViewController : BattleWorldUiController
    {
        private readonly PlayerActionButtonsView _view;

        private BattleActor _currentActor;
        private IDisposable _activeActorObserver;

        public PlayerActionsViewController(PlayerActionButtonsView view, IWorld world, IServiceLocator serviceLocator)
            : base(world, serviceLocator)
        {
            _view = view;
        }

        public override void Enable()
        {
            _view.AttackButton.onClick.AddListener(OnAttackClick);
            _view.SkillButton.onClick.AddListener(OnSkillClick);
            _view.UseItemButton.onClick.AddListener(OnItemClick);
            _view.GuardButton.onClick.AddListener(OnGuardClick);
        }
        
        private void OnAttackClick()
        {
            _view.HideAbility();
            RaiseAction("attack");
        }

        private void OnSkillClick()
        {
            _view.ShowAbility();
            _view.SubscribeOnAbilities(World.ActingActor, OnSkillSelected);
        }

        private void OnSkillSelected(ActiveBattleAbilityData data)
        {
            RaiseAction("skill", new DataProvider(data));
        }

        private void OnItemClick()
        {
            _view.HideAbility();
            RaiseAction("item", new DataProvider(0));
        }

        private void OnGuardClick()
        {
            _view.HideAbility();
            RaiseAction("guard");
        }
    }
}