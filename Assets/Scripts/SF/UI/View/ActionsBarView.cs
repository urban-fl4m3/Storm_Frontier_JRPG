using SF.Battle.Turns;
using SF.Game;
using UnityEngine;

namespace SF.UI.View
{
    public class ActionsBarView : BaseView
    {
        [SerializeField] private ActionActorIconView _playerActorActionView;
        [SerializeField] private ActionActorIconView _enemyActorActionView;
        [SerializeField] private RectTransform _waitLineContainer;
        [SerializeField] private RectTransform _actionLineContainer;
        [SerializeField] private Transform _actionsViewContainer;

        public ActionActorIconView GetTeamActorActionView(Team team)
        {
            var view =  team == Team.Player ? _playerActorActionView : _enemyActorActionView;
            var instance = Instantiate(view, _actionsViewContainer);

            return instance;
        }

        public RectTransform GetStepLineContainer(ActPhase phase)
        {
            return phase == ActPhase.Wait ? _waitLineContainer : _actionLineContainer;
        }
    }
}