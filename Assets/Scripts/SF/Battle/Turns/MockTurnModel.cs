using System;
using SF.Battle.Actors;

namespace SF.Battle.Turns
{
    public class MockTurnModel : ITurnModel
    {
        public event Action<BattleActor> TargetSelected;
        public event Action<BattleActor> TargetPicked;

        public bool IsTargetSelected()
        {
            return true;
        }

        public BattleActor GetCurrentTarget()
        {
            return null;
        }

        public void MakeTurnAction(Action onActionEnd)
        {
            onActionEnd?.Invoke();
        }
        
        public void Dispose()
        {
            
        }
    }
}