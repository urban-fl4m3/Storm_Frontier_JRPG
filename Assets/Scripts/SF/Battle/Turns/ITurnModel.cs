using System;
using SF.Battle.Actors;

namespace SF.Battle.Turns
{
    public interface ITurnModel : IDisposable
    {
        event Action<BattleActor> TargetSelected;
        event Action<BattleActor> TargetPicked;
        
        float GetActionCost();
        bool IsTargetSelected();
        BattleActor GetCurrentTarget();
        void MakeTurnAction(Action onActionEnd);
    }
}