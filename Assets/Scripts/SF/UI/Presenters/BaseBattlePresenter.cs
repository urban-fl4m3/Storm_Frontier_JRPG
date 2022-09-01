using SF.Common.Data;
using SF.Game;
using SF.UI.Models.Actions;
using SF.UI.View;

namespace SF.UI.Presenters
{
    //todo implement base presenter
    public abstract class BaseBattlePresenter<TView> : IBasePresenter
        where TView : IView
    {
        protected TView View { get; }
        protected BattleWorld World { get; }
        protected IServiceLocator ServiceLocator { get; }

        private readonly ActionBinder _actionBinder;

        protected BaseBattlePresenter(
            TView view,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder)
        {
            if (world is not BattleWorld battleWorld)
            {
                serviceLocator.Logger.LogError("Wrong world instance for ui controller");
                return;
            }

            View = view;
            World = battleWorld;
            ServiceLocator = serviceLocator;
            
            _actionBinder = actionBinder;
        }

        public abstract void Enable();
        public abstract void Disable();

        protected void RaiseAction(ActionName name, IDataProvider dataProvider = null)
        {
            _actionBinder.Raise(name, dataProvider);
        }
    }
}