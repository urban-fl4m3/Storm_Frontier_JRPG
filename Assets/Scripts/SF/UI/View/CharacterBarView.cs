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
        private IPrimaryStatResource _primaryStatResourceResource;
        
        public void StartObserve(IPrimaryStatResource primaryStatResource)
        {
            if (primaryStatResource != null)
            {
                _primaryStatResourceResource = primaryStatResource;
                
                if (_statChangeSub == null)
                {
                    _statChangeSub = primaryStatResource
                        .Current
                        .Subscribe(OnStatChange);
                }
            }
        }

        private void OnStatChange(int amount)
        {
            var maxValue = _primaryStatResourceResource.Max.Value;
            
            _statText.text = $"{amount}";
            _statFillImage.fillAmount = amount / (maxValue * 1f);
        }

        public void StopObserve()
        {
            _statChangeSub?.Dispose();
            _statChangeSub = null;
        }
    }
}