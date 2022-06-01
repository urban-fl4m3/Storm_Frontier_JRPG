using SF.Battle.Actors;
using SF.Game;
using SF.UI.Controller;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private readonly BattleHUDController _battleHUDController;
        
        public PlayerTurnAction(IServiceLocator services, BattleWorld world, BattleHUDController battleHUDController) 
            : base(services, world)
        {
            _battleHUDController = battleHUDController;
        }
        
        public override void MakeTurn(BattleActor actor)
        {
            _battleHUDController.ShowHUD();
            _battleHUDController.SomeAction += HandleSomeAction;
        }

        private void HandleSomeAction()
        {
            CompleteTurn();
        }

        protected override void Dispose()
        {
            _battleHUDController.SomeAction -= HandleSomeAction;
            _battleHUDController.HideHUD();
        }
    }
}