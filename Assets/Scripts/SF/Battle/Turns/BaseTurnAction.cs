using System;
using SF.Battle.Actors;
using SF.Game;

namespace SF.Battle.Turns
{
    public abstract class BaseTurnAction : ITurnAction
    {
        public event Action TurnCompleted;

        protected IServiceLocator Services { get; }
        protected BattleWorld World { get; }
        
        protected BaseTurnAction(IServiceLocator services, BattleWorld world)
        {
            Services = services;
            World = world;
        }
        
        public abstract void MakeTurn(BattleActor actor);

        protected abstract void Dispose();

        protected void CompleteTurn()
        {
            Dispose();
            
            TurnCompleted?.Invoke();
        }
    }
}