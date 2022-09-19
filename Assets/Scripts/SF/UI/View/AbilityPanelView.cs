using UnityEngine;

namespace SF.UI.View
{
    public class AbilityPanelView : BaseView
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private TextButtonView _buttonView;

        public RectTransform Content => _content;
        public TextButtonView ButtonView => _buttonView;
    }
}