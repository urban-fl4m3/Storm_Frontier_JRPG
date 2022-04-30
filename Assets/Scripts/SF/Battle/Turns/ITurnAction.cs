using System;
using SF.Battle.Actors;

namespace SF.Battle.Turns
{
    public interface ITurnAction
    {
        event Action TurnCompleted;
        
        void MakeTurn(BattleActor actor);
    }
}