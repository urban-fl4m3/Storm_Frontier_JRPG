using SF.UI.View;
using UnityEngine;

namespace SF.UI.Windows
{
    public class BattleHUD : MonoBehaviour, IWindow
    {
        [SerializeField] private PlayerActionButtonsView _playerActionButtonsView;
        [SerializeField] private TeamInfoView _playerTeamInfoView;
        [SerializeField] private TeamInfoView _enemyTeamInfoView;

        public PlayerActionButtonsView PlayerActionButtonsView => _playerActionButtonsView;
        public TeamInfoView PlayerTeamInfoView => _playerTeamInfoView;
        public TeamInfoView EnemyTeamInfoView => _enemyTeamInfoView;
        
        public void Show()
        {
            _playerActionButtonsView.Show();
            _playerTeamInfoView.Show();
            _enemyTeamInfoView.Show();
        }

        public void Hide()
        {
            _playerActionButtonsView.Hide();
            _playerTeamInfoView.Hide();
            _enemyTeamInfoView.Hide();
        }
        
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}