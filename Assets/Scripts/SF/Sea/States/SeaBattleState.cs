using SF.Common.Logger;
using SF.Game;
using SF.Game.States;

namespace SF.Sea.States
{
    public class SeaBattleState : GameState
    {
        public SeaBattleState(IWorld world, IDebugLogger logger) : base(world, logger)
        {
        }

        protected override void OnEnter()
        {
            Logger.Log("Entered sea battle state");
        }

        protected override void OnExit()
        {
            Logger.Log("Exited sea battle state");
        }
    }
}