using SF.Common.Logger;
using SF.Game;
using SF.Game.States;

namespace SF.Battle.States
{
    public class BattleState : GameState
    {
        public BattleState(IWorld world, IDebugLogger logger) : base(world, logger)
        {
        }

        protected override void OnEnter()
        {
            Logger.Log("Entered battle state");
        }

        protected override void OnExit()
        {
            Logger.Log("Exited battle state");
        }
    }
}