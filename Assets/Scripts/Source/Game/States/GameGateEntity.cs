using Source.Common.Data;
using Source.Services.Loggers;

namespace Source.Game.States
{
    public abstract class GameGateEntity
    {
        protected IDebugLogger Logger { get; }

        protected GameGateEntity(IDebugLogger logger)
        {
            Logger = logger;
        }

        protected abstract void OnEnter(IDataProvider dataProvider);
        protected abstract void OnExit();
    }
}