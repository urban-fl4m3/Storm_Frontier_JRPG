using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class CharacterInfoView : BaseView
    {
        [SerializeField] private Image _actorIcon;
        [SerializeField] private CharacterStatBarView healthStatBarView;
        [SerializeField] private CharacterStatBarView manaStatBarView;

        public CharacterStatBarView HealthStatBarView => healthStatBarView;
        public CharacterStatBarView ManaStatBarView => manaStatBarView;

        public void SetIcon(Sprite icon)
        {
            _actorIcon.sprite = icon;
        }
    }
}