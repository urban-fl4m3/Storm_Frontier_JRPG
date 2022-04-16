using SF.Common.Data;
using SF.Game;
using SF.Game.States;

namespace SF.Sea.States
{
    public class SeaBattleState : GameState
    {
        public SeaBattleState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public override void Enter(IDataProvider data)
        {
            ServiceLocator.Logger.Log("Entered sea battle state");
        }

        public override void Exit()
        {
            ServiceLocator.Logger.Log("Exited sea battle state");
        }
    }
}