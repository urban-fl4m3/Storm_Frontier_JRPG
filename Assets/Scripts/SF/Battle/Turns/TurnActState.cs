using UnityEngine;

namespace SF.Battle.Turns
{
    public class TurnActState
    {
        public ActPhase Phase { get; private set; }
        
        private readonly float _maxWait;

        private float _currentValue;
        private float _currentMax;
        
        public TurnActState(float maxWait, float initialTimeWaitValue = 0)
        {
            _maxWait = maxWait;
            _currentMax = maxWait;
            _currentValue = initialTimeWaitValue;

            Phase = ActPhase.Wait;
        }

        public void RaiseValue(float value)
        {
            var newValue = _currentValue + value;

            if (Phase == ActPhase.Cast)
            {
                if (newValue <= 0)
                {
                    Phase = ActPhase.Wait;
                
                    _currentMax = _maxWait;
                    newValue = _currentMax + newValue;
                    
                }
            }
            
            _currentValue = Mathf.Clamp(newValue, 0, _currentMax);
        }

        public void SetCast(float castTime)
        {
            _currentValue = 0;
            _currentMax = castTime;
            
            Phase = ActPhase.Cast;
        }

        public bool IsReadyPhase()
        {
            return _currentValue >= _currentMax;
        }
        
        public void Refresh()
        {
            _currentValue = 0;
            _currentMax = _maxWait;
            
            Phase = ActPhase.Wait;
        }
    }
}