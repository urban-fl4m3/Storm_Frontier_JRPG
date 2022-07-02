using System;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Game;
using SF.UI.View;

namespace SF.UI.Controller
{
    public class PlayerActionsViewController : BattleWorldUiController
    {
        public event Action GuardSelected = delegate { };
        public event Action AttackSelected = delegate { };
        public event Action<int> ItemSelected = delegate { };
        public event Action<ActiveBattleAbilityData> SkillSelected = delegate { };

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

        public void ShowView()
        {
            _view.Show();   
        }

        public void HideView()
        { 
            _view.Hide();
        } 
        
        public void SetCurrentActor(BattleActor actor)
        {
            _currentActor = actor;
        }
        
        private void OnAttackClick()
        {
            _view.HideAbility();
            AttackSelected?.Invoke();
        }

        private void OnSkillClick()
        {
            _view.ShowAbility();
            _view.SubscribeOnAbilities(_currentActor, OnSkillSeelcted);
        }

        private void OnSkillSeelcted(ActiveBattleAbilityData data)
        {
            SkillSelected?.Invoke(data);
        }

        private void OnItemClick()
        {
            _view.HideAbility();
            ItemSelected?.Invoke(0);
        }

        private void OnGuardClick()
        {
            _view.HideAbility();
            GuardSelected?.Invoke();
        }
    }
}