using System;
using UnityEngine;

namespace SF.UI.View
{
    [Serializable]
    public struct AbilityPanelView
    {
        [SerializeField] private RectTransform _root;
        [SerializeField] private RectTransform _content;
        [SerializeField] private AbilityButtonView _buttonView;

        public RectTransform Root => _root;
        public RectTransform Content => _content;
        public AbilityButtonView ButtonView => _buttonView;
    }
}