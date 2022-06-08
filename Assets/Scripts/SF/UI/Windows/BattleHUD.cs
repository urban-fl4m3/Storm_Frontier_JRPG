using SF.UI.View;
using UnityEngine;

namespace SF.UI.Windows
{
    public class BattleHUD : MonoBehaviour, IWindow
    {
        [SerializeField] private PlayerActionButtonsView _playerActionButtonsView;
        [SerializeField] private TeamHealthPanelView teamHealthPanelView;

        public PlayerActionButtonsView PlayerActionButtonsView => _playerActionButtonsView;
        public TeamHealthPanelView TeamHealthPanelView => teamHealthPanelView;
        
        public void Show()
        {
            _playerActionButtonsView.Show();
            teamHealthPanelView.Show();
        }

        public void Hide()
        {
            _playerActionButtonsView.Hide();
            teamHealthPanelView.Hide();
        }
        
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}