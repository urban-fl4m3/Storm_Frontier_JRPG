using System;
using SF.Common.Actors.Components.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SF.UI.View
{
    public class CharacterBarView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statText;
        [SerializeField] private Image _statFillImage;

        private IDisposable _statChangeSub;
        private BaseResourceStatComponent _resourceStatComponent;
        
        public void StartObserve(BaseResourceStatComponent resourceStatComponent)
        {
            if (resourceStatComponent != null)
            {
                _resourceStatComponent = resourceStatComponent;
                
                if (_statChangeSub == null)
                {
                    _statChangeSub = resourceStatComponent
                        .Current
                        .Subscribe(OnStatChange);
                }
            }
        }

        private void OnStatChange(int amount)
        {
            var maxValue = _resourceStatComponent.Max.Value;
            
            _statText.text = $"{amount}/{maxValue}";
            _statFillImage.fillAmount = amount / (maxValue * 1f);
        }

        public void StopObserve()
        {
            _statChangeSub?.Dispose();
            _statChangeSub = null;
        }
    }
}