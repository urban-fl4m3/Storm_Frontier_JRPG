using SF.Game;
using SF.UI.Windows;

namespace SF.UI.Controller
{
    public class TeamInfoVewController : BattleWorldUiController
    {
        private readonly Team _team;
        private readonly TeamInfoView _view;
        
        public TeamInfoVewController(
            Team team,
            TeamInfoView view,
            IWorld world,
            IServiceLocator serviceLocator)
            : base(world, serviceLocator)
        {
            _team = team;
            _view = view;
        }
        
        public override void Enable()
        {
            var actors = World.ActorsHolder.GetTeamActors(_team);

            foreach (var actor in actors)
            {
                _view.CreateHealthPanel(actor);
            }
        }
    }
}