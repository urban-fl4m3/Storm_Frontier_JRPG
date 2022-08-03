using System;
using System.Collections.Generic;
using SF.Common.Data;
using UniRx;

namespace SF.Common.States
{
    public abstract class StateMachine<TStateType, TState> where TStateType : Enum where TState : class, IState
    {
        private readonly Dictionary<TStateType, TState> _states = new();
        private readonly ReactiveProperty<TState> _currentState = new();
        
        public void SetState(TStateType stateType, IDataProvider stateMeta = null)
        {
            if (!HasState(stateType))
            {
                return;
            }

            var state = _states[stateType];
            
            if (state ==_currentState.Value)
            {
                return;
            }

            _currentState.Value?.Exit();
            _currentState.Value = state;
            _currentState.Value.Enter(stateMeta);
        }

        public TState GetCurrentState()
        {
            return _currentState.Value;
        }

        protected abstract IEnumerable<KeyValuePair<TStateType, TState>> GetStatesInfo();
        
        protected void UpdateStates()
        {
            foreach (var stateInfo in GetStatesInfo())
            {
                _states.Add(stateInfo.Key, stateInfo.Value);
            }    
        }
        
        private bool HasState(TStateType stateType)
        {
            return _states.ContainsKey(stateType);
        }
    }
}