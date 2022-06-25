using System;
using SF.UI.Pool;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class AbilityButtonView : MonoBehaviour, IPooling<AbilityButtonView>
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        public event Action<AbilityButtonView> ReturnToPool;

        public void AddActionOnClick(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void SetAbilityName(string abilityName)
        {
            _text.text = abilityName;
        }

        public void Clear()
        {
            _button.onClick.RemoveAllListeners();
        }

        public void OnReturnToPool()
        {
            ReturnToPool.Invoke(this);
            gameObject.SetActive(false);
        }
    }
}