using System.Collections.Generic;
using SF.Game;
using SF.Game.Worlds;
using SF.UI.Models.Actions;
using SF.UI.Windows;

namespace SF.UI.Presenters
{
    public class TeamInfoPresenter : BaseBattlePresenter<TeamInfoView>
    {
        private readonly Team _team;
        private readonly List<CharacterInfoPresenter> _infoPresenters = new();
        
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
                var infoView = View.CreateInfoView();
                var infoPresenter = new CharacterInfoPresenter(infoView, actor, World, ServiceLocator, ActionBinder);
                
                infoPresenter.Enable();
                
                _infoPresenters.Add(infoPresenter);
            }
            
            View.Show();
        }

        public override void Disable()
        {
            foreach (var infoPresenter in _infoPresenters)
            {
                infoPresenter.Disable();
            }
            
            View.Hide();
        }
    }
}