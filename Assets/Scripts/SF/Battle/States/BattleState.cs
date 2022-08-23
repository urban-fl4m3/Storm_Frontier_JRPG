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

        public BattleState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
            
        }

        protected override void OnEnter()
        {
            ServiceLocator.Logger.Log("Entered battle state");
            
            CreateBattleWindow();
            UpdateTurnManager();
            
            World.Run();
            
            _playerActionsViewController.Enable();
            _playerTeamInfoController.Enable();
            _enemyTeamInfoController.Enable();
        }

        protected override void OnExit()
        {
            ServiceLocator.Logger.Log("Exited battle state");
        }
        
        private void CreateBattleWindow()
        {
            var window = ServiceLocator.WindowController.Create<BattleHUD>(WindowType.Battle);
            
            //should be created in battle hud automatically
            _playerActionsViewController = new PlayerActionsViewController(window.PlayerActionButtonsView, World, ServiceLocator);
            _playerTeamInfoController = new TeamInfoVewController(Team.Player, window.PlayerTeamInfoView, World, ServiceLocator);
            _enemyTeamInfoController = new TeamInfoVewController(Team.Enemy, window.EnemyTeamInfoView, World, ServiceLocator);
        }
        
        private void UpdateTurnManager()
        {
            var playerTurnAction = new PlayerTurnAction(World.Field, World);
            var enemyTurnAction = new AiTurnAction(ServiceLocator.Logger, World);
            
            World.AddTurnAction(Team.Player, playerTurnAction);
            World.AddTurnAction(Team.Enemy, enemyTurnAction);
            
            //pass into model, bind in HUD controller
            _playerActionsViewController.BindAction("attack", playerTurnAction.HandleAttackSelected);
            _playerActionsViewController.BindAction("skill", playerTurnAction.HandleSkillSelected);
            _playerActionsViewController.BindAction("item", playerTurnAction.HandleItemSelected);
            _playerActionsViewController.BindAction("guard", playerTurnAction.HandleGuardSelected);
        }
    }
}