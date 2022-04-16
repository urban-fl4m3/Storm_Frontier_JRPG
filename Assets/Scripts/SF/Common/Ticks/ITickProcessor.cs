using System;

namespace SF.Common.Ticks
{
    public interface ITickProcessor
    {
        void Start();
        void Stop();
        void Clear();
        void AddTick(Action tick);
        void RemoveTick(Action tick);
    }
}