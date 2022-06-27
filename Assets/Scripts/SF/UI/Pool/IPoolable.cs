using System;
using UnityEngine;

namespace SF.UI.Pool
{
    public interface IPoolable
    {
        event Action ReturnToPool;
        
        GameObject Object { get; }
        
        void OnSpawn();
        void OnReturnToPool();
    }
}