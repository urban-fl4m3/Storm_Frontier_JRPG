using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class AbilityButtonView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Text _text;

        public void AddActionOnClick(UnityAction action)
        {
            _button.onClick.AddListener(action);
        }

        public void SetAbilityName(string abilityName )
        {
            _text.text = abilityName;
        }

        public void Clear()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}