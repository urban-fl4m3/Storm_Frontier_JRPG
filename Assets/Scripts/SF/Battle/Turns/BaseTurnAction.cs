using System;
using SF.Battle.Actors;
using SF.Common.Actors.Components.Status;
using SF.Game;

namespace SF.Battle.Turns
{
    public abstract class BaseTurnAction : ITurnAction
    {
        public event Action TurnCompleted;

        protected IServiceLocator Services { get; }
        protected BattleWorld World { get; }
        protected BattleActor ActingActor { get; private set; }
        
        protected BaseTurnAction(IServiceLocator services, BattleWorld world)
        {
            Services = services;
            World = world;
        }

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

        protected abstract void Dispose();

        protected void CompleteTurn()
        {
            Dispose();
            
            TurnCompleted?.Invoke();
        }

        private bool CanMakeTurn()
        {
            var stateComponent = ActingActor.Components.Get<ActorStateComponent>();

            return stateComponent.State.Value != ActorState.Dead;
        }
    }
}