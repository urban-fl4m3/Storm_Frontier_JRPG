using SF.Game;
using SF.Game.Worlds;
using SF.UI.Models.Actions;
using SF.UI.Windows;

namespace SF.UI.Presenters
{
    public class TeamInfoPresenter : BaseBattlePresenter<TeamInfoView>
    {
        private readonly Team _team;
        
        public TeamInfoPresenter(
            TeamInfoView view,
            Team team,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder)
            : base(view, world, serviceLocator, actionBinder)
        {
            _team = team;
        }
        
        public override void Enable()
        {
            var actors = World.ActorsHolder.GetTeamActors(_team);

            foreach (var actor in actors)
            {
                View.CreateHealthPanel(actor);
            }
            
            View.Show();
        }

        public override void Disable()
        {
            View.Hide();
        }
    }
}