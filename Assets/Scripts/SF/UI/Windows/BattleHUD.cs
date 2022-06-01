using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.Windows
{
    public class BattleHUD : MonoBehaviour, IWindow
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _skillButton;
        [SerializeField] private Button _useItemButton;
        [SerializeField] private Button _guardButton;
        
        public Button AttackButton => _attackButton;
        public Button SkillButton => _skillButton;
        public Button UseItemButton => _useItemButton;
        public Button GuardButton => _guardButton;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}