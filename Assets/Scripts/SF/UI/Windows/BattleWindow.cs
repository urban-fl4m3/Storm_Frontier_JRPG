using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.Windows
{
    public class BattleWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private Image _buttonPanel;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _skillButton;
        [SerializeField] private Button _useItemButton;

        public Image ButtonPanel => _buttonPanel;
        public Button AttackButton => _attackButton;
        public Button SkillButton => _skillButton;
        public Button UseItemButton => _useItemButton;
        public void Close()
        {
            Destroy(gameObject);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}