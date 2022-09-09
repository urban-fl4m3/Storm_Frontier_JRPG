using SF.Game.Stats;
using UniRx;

namespace SF.Common.Actors.Components.Stats
{
    public class PrimaryStatResource : IPrimaryStatResource
    {
        public IReadOnlyReactiveProperty<int> Max => _max;
        public IReadOnlyReactiveProperty<int> Current => _current;
     
        private readonly int _min;
        private readonly PrimaryStat _stat;
        private readonly ReactiveProperty<int> _max = new();
        private readonly ReactiveProperty<int> _current = new();

        public PrimaryStatResource(PrimaryStat stat)
        {
            _stat = stat;
            _min = 0;
        }
        
        public void Add(int amount)
        {
            if (_current.Value <= _min) return;

            var newHealth = _current.Value + amount;
            _current.Value = newHealth > _max.Value ? _max.Value : newHealth;
        }

        public void Remove(int amount)
        {
            if (_max.Value <= _min) return;
            
            var newHealth = _current.Value - amount;
            _current.Value = newHealth > _min ? newHealth : _min; 
        }
        
        public void UpdateMaxStat(int newMaxValue)
        {
            var diff = newMaxValue - _max.Value;
            _max.Value = newMaxValue;
            _current.Value += diff;
        }
    }
}