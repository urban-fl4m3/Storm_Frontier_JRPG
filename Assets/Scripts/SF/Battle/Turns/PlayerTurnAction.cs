using SF.Battle.Actors;
using SF.Game;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private BattleActor _actor;
        
        public PlayerTurnAction(IServiceLocator services, BattleWorld world) : base(services, world)
        {
        }
        
        public override void MakeTurn(BattleActor actor)
        {
            _actor = actor;
            Services.TickProcessor.AddTick(WaitForPlayerInput);
        }

        protected override void Dispose()
        {
            _actor = null;
            Services.TickProcessor.RemoveTick(WaitForPlayerInput);
        }

        private void WaitForPlayerInput()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            
            Services.Logger.Log($"Actor {_actor} turn completed");
            CompleteTurn();
        }
    }
}