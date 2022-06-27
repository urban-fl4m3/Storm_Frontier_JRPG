using SF.Battle.Turns;
using SF.Common.Data;
using SF.Game;
using SF.Game.States;
using SF.UI.Controller;
using SF.UI.Data;
using SF.UI.Windows;

namespace SF.Battle.States
{
    public class BattleState : WorldState<BattleWorld>
    {
        private TurnManager _turnManager;
        private PlayerActionsViewController _playerActionsViewController;
        private TeamHealthBarPanelController _barPanelController;
        
        public BattleState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
            
        }

        protected override void OnEnter(IDataProvider data)
        {
            ServiceLocator.Logger.Log("Entered battle state");
            
            World.Run();
            
            CreateBattleWindow();
            CreateTurnManager();
        }

        protected override void OnExit()
        {
            ServiceLocator.Logger.Log("Exited battle state");
        }
        
        private void CreateBattleWindow()
        {
            var window = ServiceLocator.WindowController.Create<BattleHUD>(WindowType.Battle);
            
            _playerActionsViewController = new PlayerActionsViewController(window.PlayerActionButtonsView, World, ServiceLocator);
            _playerActionsViewController.Enable();

            _barPanelController = new TeamHealthBarPanelController(Team.Enemy, window.TeamHealthPanelView, World, ServiceLocator);
            _barPanelController.Enable();
        }

        private void CreateTurnManager()
        {
            _turnManager = new TurnManager(ServiceLocator, World, _playerActionsViewController);
            _turnManager.PlayNextTurn();
        }
    }
}