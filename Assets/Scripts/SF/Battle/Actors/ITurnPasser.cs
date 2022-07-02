using System;

namespace SF.Battle.Actors
{
    public interface ITurnPasser
    {
        event Action TurnPassed;
    }
}