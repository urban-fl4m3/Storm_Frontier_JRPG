using SF.Battle.Turns;
using SF.Common.Data;
using SF.Game;
using SF.Game.States;
using SF.UI.Data;
using SF.UI.Factories;
using SF.UI.Windows;

namespace SF.Battle.States
{
    public class BattleState : WorldState<BattleWorld>
    {
        private readonly WindowsFactory _windowsFactory;

        private IWindow _battleWindow;
        private TurnManager _turnManager;

        public BattleState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
            _windowsFactory = ServiceLocator.FactoryHolder.Get<WindowsFactory>();
        }

        protected override void OnEnter()
        {
            ServiceLocator.Logger.Log("Entered battle state");
            
            World.Run();
            
            _battleWindow = _windowsFactory.Create(Window.Battle, new DataProvider(World, ServiceLocator));
            _battleWindow.Show();
            
            _turnManager = new TurnManager(ServiceLocator.Logger, ServiceLocator.TickProcessor, _battleWindow.Actions, World);
        }

        protected override void OnExit()
        {
            ServiceLocator.Logger.Log("Exited battle state");
        }
    }
}