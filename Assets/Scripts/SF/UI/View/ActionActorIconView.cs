using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class ActionActorIconView : BaseView
    {
        [SerializeField] private Image _icon;

        public RectTransform Rect { get; private set; }

        private void Awake()
        {
            Rect = GetComponent<RectTransform>();
        }

        public void SetActorIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}