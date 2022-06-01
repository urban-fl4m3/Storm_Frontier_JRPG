using SF.Battle.Turns;
using SF.Common.Data;
using SF.Game;
using SF.Game.States;

namespace SF.Battle.States
{
    public class BattleState : WorldState<BattleWorld>
    {
        public TurnManager TurnManager { get; private set; }
            
        public BattleState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
            
        }

        protected override void OnEnter(IDataProvider data)
        {
            ServiceLocator.Logger.Log("Entered battle state");
            
            World.Run();

            TurnManager = new TurnManager(ServiceLocator, World);
            TurnManager.PlayNextTurn();
        }

        protected override void OnExit()
        {
            ServiceLocator.Logger.Log("Exited battle state");
        }
    }
}