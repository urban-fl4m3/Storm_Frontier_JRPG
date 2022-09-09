using System;
using UniRx;

namespace SF.Common.Ticks
{
    public class TickProcessor : ITickProcessor
    {
        private event Action<long> _tickAction = delegate { };

        private IDisposable _tickSub;
        
        public void Start()
        {
            if (_tickSub != null)
            {
                return;
            }
            
            _tickSub = Observable
                .EveryUpdate()
                .Subscribe(OnTick);
        }

        public void Stop()
        {
            _tickSub?.Dispose();
            _tickSub = null;
        }

        public void Clear()
        {
            Stop();

            _tickAction = delegate { };
        }

        public void AddTick(Action<long> tick)
        {
            _tickAction += tick;
        }

        public void RemoveTick(Action<long> tick)
        {
            _tickAction -= tick;
        }

        private void OnTick(long delta)
        {
            _tickAction(delta);
        }
    }
}