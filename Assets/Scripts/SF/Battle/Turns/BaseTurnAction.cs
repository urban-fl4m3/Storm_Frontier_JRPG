using System;
using SF.Battle.Actors;
using SF.Common.Actors.Components.Status;

namespace SF.Battle.Turns
{
    public abstract class BaseTurnAction : ITurnAction
    {
        public event Action TurnCompleted;
        
        protected BattleActor ActingActor { get; private set; }

        public void MakeTurn(BattleActor actor)
        {
            ActingActor = actor;
            
            if (CanMakeTurn())
            {
                OnStartTurn();
            }
            else
            {
                CompleteTurn();
            }
        }

        protected abstract void OnStartTurn();

        protected abstract void OnTurnComplete();

        protected void CompleteTurn()
        {
            OnTurnComplete();
            
            TurnCompleted?.Invoke();
        }

        private bool CanMakeTurn()
        {
            var stateComponent = ActingActor.Components.Get<ActorStateComponent>();

            return stateComponent.State.Value != ActorState.Dead;
        }
    }
}