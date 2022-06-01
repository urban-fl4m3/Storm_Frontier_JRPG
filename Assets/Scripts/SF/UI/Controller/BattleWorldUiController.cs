using SF.Game;

namespace SF.UI.Controller
{
    public abstract class BattleWorldUiController
    {
        protected BattleWorld World { get; }
        protected IServiceLocator ServiceLocator { get; }
        
        protected BattleWorldUiController(IWorld world, IServiceLocator serviceLocator)
        {
            World = (BattleWorld) world;
            ServiceLocator = serviceLocator;

            if (World == null)
            {
                ServiceLocator.Logger.LogError("Wrong world instance for ui controller");
            }
        }
    }
}