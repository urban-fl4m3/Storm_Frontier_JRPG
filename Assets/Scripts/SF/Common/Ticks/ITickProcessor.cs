using System;

namespace SF.Common.Ticks
{
    public interface ITickProcessor
    {
        void Start();
        void Stop();
        void Clear();
        void AddTick(Action<long> tick); //todo add layers
        void RemoveTick(Action<long> tick);
    }
}