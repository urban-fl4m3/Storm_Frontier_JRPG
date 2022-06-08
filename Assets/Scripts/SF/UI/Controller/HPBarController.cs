using SF.Game;
using SF.UI.Windows;

namespace SF.UI.Controller
{
    public class HPBarController: BattleWorldUiController
    {
        private readonly HPBarHUD _hud;
        
        public HPBarController(HPBarHUD hud,IWorld world, IServiceLocator serviceLocator) : base(world, serviceLocator)
        {
            _hud = hud;
        }

        public void Init()
        {
            var actors = World.ActingActors;

            foreach (var actor in actors)
            {
                _hud.CreateHPPanel(actor);
            }
        }
    }
}