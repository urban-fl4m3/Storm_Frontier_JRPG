using System.Collections.Generic;
using SF.Game;
using SF.UI.Presenters;
using SF.UI.View;
using UnityEngine;

namespace SF.UI.Windows
{
    public class BattleHUD : BaseWindow
    {
        [SerializeField] private PlayerActionButtonsView _playerActionButtonsView;
        [SerializeField] private TeamInfoView _playerTeamInfoView;
        [SerializeField] private TeamInfoView _enemyTeamInfoView;

        protected override IEnumerable<IBasePresenter> ResolvePresenters()
        {
            yield return new PlayerActionsPresenter(_playerActionButtonsView, World, Services, Actions);
            yield return new TeamInfoPresenter(_playerTeamInfoView, Team.Player, World, Services, Actions);
            yield return new TeamInfoPresenter(_enemyTeamInfoView, Team.Enemy, World, Services, Actions);
        }
    }
}