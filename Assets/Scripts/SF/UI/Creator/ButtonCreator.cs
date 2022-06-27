using System.Collections.Generic;
using SF.UI.Pool;
using SF.UI.View;
using UnityEngine;

namespace SF.UI.Creator
{
    public class ButtonCreator
    {
        private readonly List<AbilityButtonView> _abilityButtonViews = new List<AbilityButtonView>();
        private readonly ButtonPool _buttonPool;

        private Transform _root;

        public ButtonCreator(Transform root, AbilityButtonView prefab)
        {
            _buttonPool = new ButtonPool(prefab);
            _root = root;
        }

        public AbilityButtonView Get()
        {
            var button = _buttonPool.Get();
            
            if(!_abilityButtonViews.Contains(button))
                _abilityButtonViews.Add(button);
            
            button.Clear();
            button.transform.SetParent(_root);
            button.transform.localScale = Vector3.one;
            button.transform.localPosition = Vector3.zero;
            button.gameObject.SetActive(true);

            return button;
        }

        public void Clear()
        {
            for (int i = 0; i < _abilityButtonViews.Count; i++)
            {
                _abilityButtonViews[i].OnReturnToPool();
                _abilityButtonViews[i].Clear();
            }
        }
    }
}