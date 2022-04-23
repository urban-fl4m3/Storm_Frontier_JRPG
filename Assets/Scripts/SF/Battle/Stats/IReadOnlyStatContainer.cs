using System;

namespace SF.Battle.Stats
{
    public interface IReadOnlyStatContainer<TStat> where TStat : Enum
    {
        int GetStat(TStat stat);
    }
}