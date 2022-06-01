using System;
using SF.Game;
using SF.UI.Windows;
using UnityEngine;

namespace SF.UI.Controller
{
    public class BattleHUDController : BattleWorldUiController
    {
        public event Action SomeAction;
        
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
            Debug.Log("Attack click");
        }

        private void OnSkillClick()
        {
            Debug.Log("Skill click");
        }

        private void OnItemClick()
        {
            Debug.Log("Use Item click");
        }

        private void OnGuardClick()
        {
            Debug.Log("Guard click!");
            
            SomeAction?.Invoke();
        }
    }
}