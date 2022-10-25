using Cysharp.Threading.Tasks;
using Source.Common.Data;
using Source.Common.States;
using Source.Services.Loggers;
using UnityEngine.SceneManagement;

namespace Source.Game.States
{
    public abstract class SceneState : GameGateEntity, IState
    {
        protected SceneState(IDebugLogger logger) : base(logger)
        {
            
        }

        async UniTaskVoid IState.Enter(IDataProvider dataProvider)
        {
            await SceneManager.LoadSceneAsync(GetScenePath(), LoadSceneMode.Additive);
            
            OnEnter(dataProvider);
        }

        void IState.Exit()
        {
            
        }

        protected abstract string GetScenePath();
    }
}