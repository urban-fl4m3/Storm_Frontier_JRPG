using System;
using SF.Battle.Actors;
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
        [SerializeField] private Image _actorIcon;
        [SerializeField] private Text _actorName;

        private HealthComponent _observableHealthComponent;

        private IDisposable _healthChangeSub;
        
        public void ObserveActorHealth(BattleActor actor)
        {
            _observableHealthComponent = actor.Components.Get<HealthComponent>();

            var visualParameter = actor.MetaData.Info.Config;

            _actorName.text = visualParameter.Name;
            _actorIcon.sprite = visualParameter.Icon;
            
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