using SF.Common.Data;

namespace SF.Common.States
{
    public interface IState
    {
        void Enter(IDataProvider data);
        void Exit();
    }
}