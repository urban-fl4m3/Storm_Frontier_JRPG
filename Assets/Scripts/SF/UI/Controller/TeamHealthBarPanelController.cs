using System.Linq;
using SF.Game;
using SF.UI.Windows;

namespace SF.UI.Controller
{
    public class TeamHealthBarPanelController: BattleWorldUiController
    {
        private readonly Team _team;
        private readonly TeamHealthPanelView _view;
        
        public TeamHealthBarPanelController(Team team, TeamHealthPanelView view, IWorld world, IServiceLocator serviceLocator)
            : base(world, serviceLocator)
        {
            _team = team;
            _view = view;
        }

        public void CreateHealthPanels()
        {
            var actors = World.ActingActors.Where(x => x.Team == _team);

            foreach (var actor in actors)
            {
                _view.CreateHealthPanel(actor);
            }
        }
    }
}