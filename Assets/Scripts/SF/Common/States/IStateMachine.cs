using UniRx;

namespace SF.Common.States
{
    public interface IStateMachine
    {
        IReadOnlyReactiveProperty<IState> CurrentState { get; }
    }
}