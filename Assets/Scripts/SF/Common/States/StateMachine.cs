using System;
using System.Collections.Generic;
using UniRx;

namespace SF.Common.States
{
    public abstract class StateMachine<TStateType> : IStateMachine where TStateType : Enum
    {
        public IReadOnlyReactiveProperty<IState> CurrentState => _currentState;

        protected readonly Dictionary<TStateType, IState> _states = new Dictionary<TStateType, IState>();

        private readonly ReactiveProperty<IState> _currentState = new ReactiveProperty<IState>();

        protected StateMachine()
        {
            UpdateStates();
        }
        
        protected void ChangeState(TStateType stateType)
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
            _currentState.Value.Enter();
        }

        protected abstract IEnumerable<KeyValuePair<TStateType, IState>> GetStatesInfo();

        private void UpdateStates()
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