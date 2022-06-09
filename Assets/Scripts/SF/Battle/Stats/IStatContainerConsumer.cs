using System;

namespace SF.Battle.Stats
{
    public interface IStatContainerConsumer<in TStat> where TStat : Enum
    {
        void SetStatValue(TStat stat, int value);
        void AddStatValue(TStat stat, int value);
    }
}