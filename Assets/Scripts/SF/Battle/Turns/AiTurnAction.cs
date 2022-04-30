using SF.Battle.Actors;
using SF.Game;

namespace SF.Battle.Turns
{
    public class AiTurnAction : BaseTurnAction
    {
        public AiTurnAction(IServiceLocator services, BattleWorld world) : base(services, world)
        {
            
        }
        
        public override void MakeTurn(BattleActor actor)
        {
            Services.Logger.Log($"Actor {actor} turn completed");
            CompleteTurn();
        }

        protected override void Dispose()
        {
            
        }
    }
}