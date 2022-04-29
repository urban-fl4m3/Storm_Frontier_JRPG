using System;

namespace SF.Battle.Stats
{
    public interface IReadOnlyStatContainer<in TStat> where TStat : Enum
    {
        int GetStat(TStat stat);
    }
}