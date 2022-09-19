using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class PlayerActionButtonsView : BaseView
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _skillButton;
        [SerializeField] private Button _useItemButton;
        [SerializeField] private Button _guardButton;
        [SerializeField] private AbilityPanelView _abilityPanelView;
        
        public Button AttackButton => _attackButton;
        public Button SkillButton => _skillButton;
        public Button UseItemButton => _useItemButton;
        public Button GuardButton => _guardButton;

        public AbilityPanelView AbilityPanelView => _abilityPanelView;
    }
}