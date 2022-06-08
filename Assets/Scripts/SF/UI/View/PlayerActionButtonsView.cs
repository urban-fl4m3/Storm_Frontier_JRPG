using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class PlayerActionButtonsView : SerializedMonoBehaviour, IView
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
    }
}