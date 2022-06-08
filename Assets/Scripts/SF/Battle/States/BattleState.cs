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
        private BattleHUDController _battleHUDController;
        private HPBarController _barController;
        
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
            var hpBar = ServiceLocator.WindowController.Create<HPBarHUD>(WindowType.HpBar);

            _battleHUDController = new BattleHUDController(window, World, ServiceLocator);
            _battleHUDController.Init();

            _barController = new HPBarController(hpBar, World, ServiceLocator);
            _barController.Init();
        }

        private void CreateTurnManager()
        {
            _turnManager = new TurnManager(ServiceLocator, World, _battleHUDController);
            _turnManager.PlayNextTurn();
        }
    }
}