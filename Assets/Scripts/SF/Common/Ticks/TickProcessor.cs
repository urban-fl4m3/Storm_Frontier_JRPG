using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRxExt = UniRx.ObservableExtensions;

namespace SF.Common.Ticks
{
    public class TickProcessor : ITickProcessor
    {
        private readonly List<Action> _ticks = new();
        private readonly Queue<Action> _ticksToAdd = new();
        private readonly Queue<Action> _ticksToRemove = new();

        private IDisposable _tickSub;
        
        public void Start()
        {
            if (_tickSub != null)
            {
                return;
            }
            
            _tickSub = UniRxExt.Subscribe(Observable.EveryUpdate(), OnTick);
        }

        public void Stop()
        {
            _tickSub?.Dispose();
            _tickSub = null;
        }

        public void Clear()
        {
            Stop();
            
            _ticksToRemove.Clear();
            _ticksToAdd.Clear();
            _ticks.Clear();
        }

        public void AddTick(Action tick)
        {
            _ticksToAdd.Enqueue(tick);
        }

        public void RemoveTick(Action tick)
        {
            _ticksToRemove.Enqueue(tick);
        }

        private void OnTick(long l)
        {
            while (_ticksToRemove.Any())
            {
                _ticks.Remove(_ticksToRemove.Dequeue());
            }

            while (_ticksToAdd.Any())
            {
                _ticks.Add(_ticksToAdd.Dequeue());
            }

            foreach (var tick in _ticks)
            {
                tick();
            }
        }
    }
}