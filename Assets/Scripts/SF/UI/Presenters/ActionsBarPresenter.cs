using System.Collections.Generic;
using SF.Battle.Turns;
using SF.Game;
using SF.Game.Worlds;
using SF.UI.Models.Actions;
using SF.UI.View;
using UnityEngine;

namespace SF.UI.Presenters
{
    public class ActionsBarPresenter : BaseBattlePresenter<ActionsBarView>
    {
        private readonly Dictionary<ITurnAction, ActionActorIconView> _actionViews = new();
        
        public ActionsBarPresenter(
            ActionsBarView view,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder) 
            : base(view, world, serviceLocator, actionBinder)
        {
            
        }

        public override void Enable()
        {
            View.Show();
            
            CreateIconViews();
            
            World.Turns.ActionsUpdated += HandleActionsUpdated;
        }

        public override void Disable()
        {
            View.Hide();

            World.Turns.ActionsUpdated -= HandleActionsUpdated;
        }

        private void CreateIconViews()
        {
            foreach (var action in World.Turns.RegisteredActions)
            {
                if (_actionViews.ContainsKey(action))
                {
                    return;
                }
                
                var actingActor = action.ActingActor;
                var view = View.GetTeamActorActionView(actingActor.Team);
                
                view.SetActorIcon(actingActor.MetaData.Info.Config.Icon);
                
                _actionViews.Add(action, view);
            }
        }

        private void HandleActionsUpdated()
        {
            foreach (var action in World.Turns.RegisteredActions)
            {
                var view = _actionViews[action];
                
                var currentProgress = action.GetCurrentProgress();
                var maxProgress = action.GetMaxProgress();
                var viewContainer = View.GetStepLineContainer(action.Phase);

                var xProgress = Mathf.Lerp(
                    viewContainer.offsetMin.x,
                    viewContainer.offsetMax.x,
                    currentProgress / maxProgress);

                view.Rect.anchoredPosition = new Vector2(xProgress, view.Rect.anchoredPosition.y);
            }
        }
    }
}