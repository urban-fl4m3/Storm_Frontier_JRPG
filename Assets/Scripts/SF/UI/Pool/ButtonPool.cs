using System.Collections.Generic;
using System.Linq;
using SF.UI.View;
using Sirenix.Utilities;
using Object = UnityEngine.Object;

namespace SF.UI.Pool
{
    public class ButtonPool
    {
        private Queue<AbilityButtonView> _inactiveButtons = new Queue<AbilityButtonView>();
        private AbilityButtonView _prefab;

        public ButtonPool(AbilityButtonView view)
        {
            _prefab = view;
        }

        public AbilityButtonView Get()
        {
            if(_inactiveButtons.Count == 0)
                Create();

            var button = _inactiveButtons.Dequeue();
            
            return button;
        }

        private void Create()
        {
            var button = Object.Instantiate(_prefab);
            button.ReturnToPool += OnReturnToPool;
            
            _inactiveButtons.Enqueue(button);
        }

        private void OnReturnToPool(AbilityButtonView button)
        {
            _inactiveButtons.Enqueue(button);
        }
    }
}