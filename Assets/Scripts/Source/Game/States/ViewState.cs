using Cysharp.Threading.Tasks;
using Source.Common.Data;
using Source.Common.States;
using Source.Services.Loggers;

namespace Source.Game.States
{
    public abstract class ViewState : GameGateEntity, IState
    {
        protected ViewState(IDebugLogger logger) : base(logger)
        {
        }

        UniTaskVoid IState.Enter(IDataProvider data)
        {
            OnEnter(data);

            return new UniTaskVoid();
        }

        void IState.Exit()
        {
            OnExit();   
        }
    }
}