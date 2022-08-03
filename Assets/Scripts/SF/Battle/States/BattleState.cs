using SF.Battle.Turns;
using SF.Game;
using SF.Game.States;
using SF.UI.Controller;
using SF.UI.Data;
using SF.UI.Windows;

namespace SF.Battle.States
{
    public class BattleState : WorldState<BattleWorld>
    {
        private PlayerActionsViewController _playerActionsViewController;
        private TeamInfoVewController _playerTeamInfoController;
        private TeamInfoVewController _enemyTeamInfoController;
        private TurnManager _turnManager;
        
        public BattleState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
            
        }

        protected override void OnEnter()
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

            _playerTeamInfoController = new TeamInfoVewController(Team.Player, window.PlayerTeamInfoView, World, ServiceLocator);
            _playerTeamInfoController.Enable();
            
            _enemyTeamInfoController = new TeamInfoVewController(Team.Enemy, window.EnemyTeamInfoView, World, ServiceLocator);
            _enemyTeamInfoController.Enable();
        }
        
        private void CreateTurnManager()
        {
            _turnManager = new TurnManager(ServiceLocator.Logger, World.Field, ServiceLocator.CameraHolder, 
                World.ActorsHolder, _playerActionsViewController);
            _turnManager.PlayNextTurn();
        }
    }
}