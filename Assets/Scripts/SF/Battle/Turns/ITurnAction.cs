using System;
using SF.Battle.Actors;
using SF.Common.Actors;

namespace SF.Battle.Turns
{
    public interface ITurnAction
    {
        event Action TurnStarted;
        event Action TurnCompleted;

        event Action<IActor> ActorSelected; 

        void MakeTurn(BattleActor actor);
    }
}