using System;
using SF.UI.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class ButtonView : MonoBehaviour, IPoolable
    {
        [SerializeField] private Button _button;
        
        public event Action ReturnToPool = delegate {  };
        
        public GameObject Object => gameObject;
        
        public void AddActionOnClick(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void OnSpawn()
        {
            gameObject.SetActive(true);
        }

        public void OnReturnToPool()
        {
            ReturnToPool.Invoke();
            gameObject.SetActive(false);
            Clear();
        }

        public void ChangeInteractable(bool isInteractable)
        {
            _button.interactable = isInteractable;
        }

        private void Clear()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}