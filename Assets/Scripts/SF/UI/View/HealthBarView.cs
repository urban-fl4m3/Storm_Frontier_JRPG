using System;
using SF.Common.Actors;
using SF.Common.Actors.Components.Stats;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace SF.UI.View
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Text _healthText;
        [SerializeField] private Image _fillImage;

        private HealthComponent _observableHealthComponent;

        private IDisposable _healthChangeSub;
        
        public void ObserveActorHealth(IActor actor)
        {
            _observableHealthComponent = actor.Components.Get<HealthComponent>();
            
            StartObservingHealth();
        }

        private void OnHealthChanged(int amount)
        {
            var maxHealth = _observableHealthComponent.MaxHealth.Value;
            
            _healthText.text = $"{amount}/{maxHealth}";
            _fillImage.fillAmount = amount / (maxHealth * 1f);
        }

        private void StartObservingHealth()
        {
            if (_observableHealthComponent != null)
            {
                if (_healthChangeSub == null)
                {
                    _healthChangeSub = _observableHealthComponent.CurrentHealth.Subscribe(OnHealthChanged);
                }
                
                // OnHealthChanged(_observableHealthComponent.CurrentHealth.Value);
            }
        }
        
        private void OnEnable()
        {
            StartObservingHealth();
        }

        private void OnDisable()
        {
            _healthChangeSub?.Dispose();
            _healthChangeSub = null;
        }
    }
}