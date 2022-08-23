using System;
using System.Collections.Generic;
using SF.Common.Data;
using SF.Game;

namespace SF.UI.Controller
{
    public abstract class BattleWorldUiController
    {
        protected BattleWorld World { get; }
        protected IServiceLocator ServiceLocator { get; }

        private Dictionary<string, Action<IDataProvider>> _bindedActions = new();
        
        protected BattleWorldUiController(IWorld world, IServiceLocator serviceLocator)
        {
            World = (BattleWorld) world;
            ServiceLocator = serviceLocator;

            if (World == null)
            {
                ServiceLocator.Logger.LogError("Wrong world instance for ui controller");
            }
        }

        public abstract void Enable();

        public void BindAction(string actionName, Action<IDataProvider> action)
        {
            _bindedActions.Add(actionName, action);
        }

        protected void RaiseAction(string actionName, IDataProvider dataProvider = null)
        {
            _bindedActions[actionName].Invoke(dataProvider);
        }
    }
}