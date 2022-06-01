using SF.Battle.States;
using SF.Game;
using SF.Game.States;

namespace SF.UI.Controller
{
    public abstract class BattleWorldUiController
    {
        protected BattleWorld World { get; }
        protected BattleState State { get; }
        protected IServiceLocator ServiceLocator { get; }
        
        protected BattleWorldUiController(IWorld world, GameState state, IServiceLocator serviceLocator)
        {
            World = (BattleWorld) world;
            State = (BattleState) state;
            ServiceLocator = serviceLocator;

            if (World == null)
            {
                ServiceLocator.Logger.LogError("Wrong world instance for ui controller");
            }

            if (State == null)
            {
                ServiceLocator.Logger.LogError("Wrong state instance for ui controller");
            }
        }
    }
}