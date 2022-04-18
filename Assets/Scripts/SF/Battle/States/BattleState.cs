using SF.Common.Data;
using SF.Game;
using SF.Game.States;

namespace SF.Battle.States
{
    public class BattleState : WorldState<BattleWorld>
    {
        public BattleState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        protected override void OnEnter(IDataProvider data)
        {
            ServiceLocator.Logger.Log("Entered battle state");
            
            World.Run();
        }

        protected override void OnExit()
        {
            ServiceLocator.Logger.Log("Exited battle state");
        }
    }
}