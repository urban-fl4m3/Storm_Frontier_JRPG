using System;
using System.Collections.Generic;
using Source.Common.Data;

namespace Source.Common.States
{
    public abstract class StateMachine : IChangeStateResolver
    {
        protected Dictionary<Type, IState> States { get; } = new();
        
        private IState _currentState ;
        
        public void ChangeState<TState>(IDataProvider stateMeta = null) where TState : IState
        {
            var stateType = typeof(TState);
            
            if (!HasState(stateType))
            {
                return;
            }

            var state = States[stateType];
            
            if (state ==_currentState)
            {
                return;
            }

            _currentState?.Exit();
            _currentState = state;
            _currentState.Enter(stateMeta).Forget();
        }
        
        private bool HasState(Type stateType)
        {
            return States.ContainsKey(stateType);
        }
    }
}