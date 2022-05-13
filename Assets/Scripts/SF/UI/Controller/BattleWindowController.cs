using SF.UI.Windows;
using UnityEngine;

namespace SF.UI.Controller
{
    public class BattleWindowController
    {
        private readonly BattleWindow _window;

        public BattleWindowController(BattleWindow window)
        {
            _window = window;
        }

        public void Init()
        {
            _window.AttackButton.onClick.AddListener(OnAttackClick);
            _window.UseItemButton.onClick.AddListener(OnItemClick);
            _window.UseItemButton.onClick.AddListener(OnSkillClick);
        }

        private void OnAttackClick()
        {
            Debug.Log("Attack click");
        }

        private void OnItemClick()
        {
            Debug.Log("Use Item click");
        }

        private void OnSkillClick()
        {
            Debug.Log("Skill click");
        }
    }
}