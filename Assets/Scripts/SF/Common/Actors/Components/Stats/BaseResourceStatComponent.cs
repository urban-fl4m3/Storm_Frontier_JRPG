using System;
using SF.Game.Stats;
using UniRx;

namespace SF.Common.Actors.Components.Stats
{
    public abstract class BaseResourceStatComponent : ActorComponent
    {
        protected abstract int Min { get; }
        protected abstract PrimaryStat Stat { get; }
        
        public IReadOnlyReactiveProperty<int> Max => _max;
        public IReadOnlyReactiveProperty<int> Current => _current;
        
        private readonly ReactiveProperty<int> _max = new();
        private readonly ReactiveProperty<int> _current = new();

        private StatsContainerComponent _statsDataContainer;

        public void Add(int amount)
        {
            if (_current.Value <= Min) return;

            var newHealth = _current.Value + amount;
            _current.Value = newHealth > _max.Value ? _max.Value : newHealth;
        }

        public void Remove(int amount)
        {
            if (_max.Value <= Min) return;
            
            var newHealth = _current.Value - amount;
            _current.Value = newHealth > Min ? newHealth : Min; 
        }
        
        protected override void OnInit()
        {
            IDisposable statsContainerInitSub = null;

            _statsDataContainer = Owner.Components.Get<StatsContainerComponent>();

            statsContainerInitSub = _statsDataContainer
                .IsInit
                .Subscribe(isInit =>
                {
                    if (!isInit) return;
                    
                    UpdateMaxStat();
                    statsContainerInitSub?.Dispose();
                    base.OnInit();
                });
            
            _statsDataContainer.PrimaryStatChanged += OnStatChange;
        }

        private void OnStatChange(PrimaryStat stat)
        {
            if (stat == Stat)
            {
                UpdateMaxStat();
            }
        }

        private void UpdateMaxStat()
        {
            var newMaxValue = _statsDataContainer.GetStat(Stat);
            var diff = newMaxValue - _max.Value;
            _max.Value = newMaxValue;
            _current.Value += diff;
        }
    }
}