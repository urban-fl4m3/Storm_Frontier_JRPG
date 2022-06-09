using System;
using SF.Game.Stats;
using UniRx;

namespace SF.Common.Actors.Components.Stats
{
    public class HealthComponent : ActorComponent
    {
        private const int MIN_HEALTH = 0;
        
        public IReadOnlyReactiveProperty<int> MaxHealth => _maxHealth;
        public IReadOnlyReactiveProperty<int> CurrentHealth => _currentHealth;
        
        private readonly ReactiveProperty<int> _maxHealth = new ReactiveProperty<int>();
        private readonly ReactiveProperty<int> _currentHealth = new ReactiveProperty<int>();

        private StatsContainerComponent _statsDataContainer;

        public void AddHealth(int amount)
        {
            if (_currentHealth.Value <= MIN_HEALTH) return;

            var newHealth = _currentHealth.Value + amount;
            _currentHealth.Value = newHealth > _maxHealth.Value ? _maxHealth.Value : newHealth;
        }

        public void RemoveHealth(int amount)
        {
            if (_currentHealth.Value <= MIN_HEALTH) return;
            
            var newHealth = _currentHealth.Value - amount;
            _currentHealth.Value = newHealth > MIN_HEALTH ? newHealth : MIN_HEALTH; 
        }
        
        protected override void OnInit()
        {
            IDisposable statsContainerInitSub = null;

            _statsDataContainer = Owner.Components.Get<StatsContainerComponent>();

            statsContainerInitSub = _statsDataContainer
                .IsInit
                .SkipLatestValueOnSubscribe()
                .Subscribe(isInit =>
                {
                    if (!isInit) return;
                    
                    UpdateMaxHealth();
                    statsContainerInitSub?.Dispose();
                    base.OnInit();
                });
        }

        private void UpdateMaxHealth()
        {
            _maxHealth.Value = _statsDataContainer.GetStat(PrimaryStat.HP);
            _currentHealth.Value = _maxHealth.Value;
        }
    }
}