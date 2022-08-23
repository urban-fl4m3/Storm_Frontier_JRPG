using System;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Common.Actors;
using SF.Common.Actors.Components.Status;

namespace SF.Battle.Turns
{
    public abstract class BaseTurnAction : ITurnAction
    {
        public event Action TurnStarted;
        public event Action TurnCompleted;
        public event Action<IActor> ActorSelected;

        protected BattleActor ActingActor { get; private set; }
        protected IBattleActorsHolder ActorsHolder { get; }

        private Action _action;
            
        protected BaseTurnAction(IBattleActorsHolder actorsHolder)
        {
            ActorsHolder = actorsHolder;
        }
        
        public void MakeTurn(BattleActor actor)
        {
            ActingActor = actor;
            
            if (CanMakeTurn())
            {
                TurnStarted?.Invoke();
                
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

        protected void SelectActor(IActor actor)
        {
            ActorSelected?.Invoke(actor);
        }

        private bool CanMakeTurn()
        {
            var stateComponent = ActingActor.Components.Get<ActorStateComponent>();

            return stateComponent.State.Value != ActorState.Dead;
        }
    }
}