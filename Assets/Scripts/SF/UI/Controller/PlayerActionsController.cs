using System;
using SF.Game;
using SF.UI.View;

namespace SF.UI.Controller
{
    public class PlayerActionsController : BattleWorldUiController
    {
        public event Action AttackSelected;
        public event Action<int> SkillSelected;
        public event Action<int> ItemSelected;
        public event Action GuardSelected;
        
        private readonly PlayerActionButtonsView _view;

        private IDisposable _activeActorObserver;
        
        public PlayerActionsController(PlayerActionButtonsView view, IWorld world,IServiceLocator serviceLocator) 
            : base(world, serviceLocator)
        {
            _view = view;
        }

        public void Init()
        {
            _view.AttackButton.onClick.AddListener(OnAttackClick);
            _view.SkillButton.onClick.AddListener(OnSkillClick);
            _view.UseItemButton.onClick.AddListener(OnItemClick);
            _view.GuardButton.onClick.AddListener(OnGuardClick);
        }

        public void ShowView() => _view.Show();

        public void HideView() => _view.Hide();
        
        private void OnAttackClick()
        {
            AttackSelected?.Invoke();
        }

        private void OnSkillClick()
        {
            SkillSelected?.Invoke(0);
        }

        private void OnItemClick()
        {
            ItemSelected?.Invoke(0);
        }

        private void OnGuardClick()
        {
            GuardSelected?.Invoke();
        }
    }
}