using System;
using SF.Game;
using SF.UI.Windows;

namespace SF.UI.Controller
{
    public class BattleHUDController : BattleWorldUiController
    {
        public event Action AttackSelected;
        public event Action<int> SkillSelected;
        public event Action<int> ItemSelected;
        public event Action GuardSelected;
        
        private readonly BattleHUD _hud;

        private IDisposable _activeActorObserver;
        
        public BattleHUDController(BattleHUD hud, IWorld world,IServiceLocator serviceLocator) 
            : base(world, serviceLocator)
        {
            _hud = hud;
        }

        public void Init()
        {
            _hud.AttackButton.onClick.AddListener(OnAttackClick);
            _hud.SkillButton.onClick.AddListener(OnSkillClick);
            _hud.UseItemButton.onClick.AddListener(OnItemClick);
            _hud.GuardButton.onClick.AddListener(OnGuardClick);
        }

        public void ShowHUD() => _hud.Show();

        public void HideHUD() => _hud.Hide();
        
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