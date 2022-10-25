using Source.Common.Data;

namespace Source.Common.States
{
    public interface IChangeStateResolver
    {
        void ChangeState<TState>(IDataProvider stateMeta = null) where TState : IState;
    }
}