using System;
using System.Collections.Generic;
using Sirenix.Serialization;

namespace SF.Game.Stats
{
    [Serializable]
    public class StatContainerData<TStat> where TStat : Enum
    {
        [OdinSerialize] private Dictionary<TStat, float> _stats;
        
        public IReadOnlyDictionary<TStat, float> Stats => _stats;
    }
}