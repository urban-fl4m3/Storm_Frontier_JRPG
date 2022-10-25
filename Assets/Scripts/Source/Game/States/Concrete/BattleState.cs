using Source.Common.Data;
using Source.Services.Loggers;

namespace Source.Game.States.Concrete
{
    public class BattleState : SceneState
    {
        public BattleState(IDebugLogger logger) : base(logger)
        {
            
        }

        protected override void OnEnter(IDataProvider dataProvider)
        {
            Logger.Log("Hello, battle!");
        }

        protected override void OnExit()
        {
            
        }

        protected override string GetScenePath()
        {
            return "Scenes/Roguelike/BattleScene";
        }
    }
}