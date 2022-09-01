using System;
using System.Collections.Generic;
using SF.Common.Data;

namespace SF.UI.Models.Actions
{
    public class ActionBinder : IReadonlyActionBinder
    {
        private readonly Dictionary<ActionName, Action<IDataProvider>> _bindedActions = new();

        public void Subscribe(ActionName name, Action<IDataProvider> action)
        {
            if (!_bindedActions.ContainsKey(name))
            {
                _bindedActions.Add(name, delegate { });
            }
            
            _bindedActions[name] += action;
        }

        public void Unsubscribe(ActionName name, Action<IDataProvider> action)
        {
            if (_bindedActions.ContainsKey(name))
            {
                _bindedActions[name] -= action;
            }
        }

        public void Raise(ActionName name, IDataProvider dataProvider)
        {
            if (_bindedActions.ContainsKey(name))
            {
                _bindedActions[name].Invoke(dataProvider);
            }
        }
    }
}