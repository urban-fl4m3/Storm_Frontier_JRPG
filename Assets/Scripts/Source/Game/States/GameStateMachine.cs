using System.Collections.Generic;
using Source.Common.States;

namespace Source.Game.States
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(IEnumerable<IState> states)
        {
            foreach (var state in states)
            {
                States.Add(state.GetType(), state);
            }
        }
    }
}