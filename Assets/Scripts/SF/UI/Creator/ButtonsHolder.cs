using System.Collections.Generic;
using SF.UI.Pool;
using SF.UI.View;
using UnityEngine;

namespace SF.UI.Creator
{
    public class ButtonsHolder<TButton> where TButton : ButtonView
    {
        private readonly List<TButton> _buttons = new List<TButton>();
        private readonly ObjectPool<TButton> _objectPool;

        private readonly Transform _root;

        public ButtonsHolder(Transform root, TButton prefab)
        {
            _objectPool = new ObjectPool<TButton>(prefab);
            _root = root;
        }

        public TButton Get()
        {
            var button = _objectPool.Get();
            
            _buttons.Add(button);
            
            button.transform.SetParent(_root, false);

            return button;
        }

        public void Clear()
        {
            foreach (var button in _buttons)
            {
                button.OnReturnToPool();
            }
        }
    }
}