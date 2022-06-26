using System;

namespace SF.UI.Pool
{
    public interface IPooling<T>
    {
        event Action<T> ReturnToPool;
        void OnReturnToPool();
    }
}