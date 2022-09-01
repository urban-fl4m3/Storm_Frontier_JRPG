using SF.Common.Data;
using SF.Game;
using SF.Game.Player;
using SF.Game.States;

namespace SF.Sea.States
{
    public class SeaBattleState : GameState
    {
        public SeaBattleState(IServiceLocator services, IPlayerState playerState) : base(services, playerState)
        {
        }

        public override void Enter(IDataProvider data)
        {
            Services.Logger.Log("Entered sea battle state");
        }

        public override void Exit()
        {
            Services.Logger.Log("Exited sea battle state");
        }
    }
}